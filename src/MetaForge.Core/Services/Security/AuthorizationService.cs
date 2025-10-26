using MetaForge.Core.Repositories;

namespace MetaForge.Core.Services.Security;

/// <summary>
/// Implementación del servicio de autorización
/// </summary>
public class AuthorizationService : IAuthorizationService
{
    private readonly IUserRepository _userRepository;
    private readonly Dictionary<int, (List<string> roles, List<string> permissions, DateTime cachedAt)> _cache = new();
    private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(5);

    public AuthorizationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Verifica si un usuario tiene un permiso específico
    /// </summary>
    public async Task<bool> HasPermissionAsync(int userId, string permission)
    {
        var permissions = await GetUserPermissionsAsync(userId);
        return permissions.Contains(permission, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Verifica si un usuario tiene un rol específico
    /// </summary>
    public async Task<bool> HasRoleAsync(int userId, string roleName)
    {
        var roles = await GetUserRolesAsync(userId);
        return roles.Contains(roleName, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Verifica si un usuario tiene acceso a un recurso con una acción
    /// </summary>
    public async Task<bool> CanAccessAsync(int userId, string resource, string action)
    {
        var permissions = await GetUserPermissionsAsync(userId);
        
        // Formato: resource.action (ej: "users.create", "tables.read")
        var fullPermission = $"{resource}.{action}";
        
        // Verificar permiso específico
        if (permissions.Contains(fullPermission, StringComparer.OrdinalIgnoreCase))
            return true;

        // Verificar permiso wildcard (resource.*)
        var wildcardPermission = $"{resource}.*";
        if (permissions.Contains(wildcardPermission, StringComparer.OrdinalIgnoreCase))
            return true;

        // Verificar permiso super admin (*.*)
        if (permissions.Contains("*.*", StringComparer.OrdinalIgnoreCase))
            return true;

        return false;
    }

    /// <summary>
    /// Obtiene todos los permisos de un usuario (con caché)
    /// </summary>
    public async Task<List<string>> GetUserPermissionsAsync(int userId)
    {
        // Verificar caché
        if (_cache.TryGetValue(userId, out var cached))
        {
            if (DateTime.UtcNow - cached.cachedAt < _cacheExpiration)
            {
                return cached.permissions;
            }
            else
            {
                _cache.Remove(userId);
            }
        }

        // Obtener permisos desde BD
        var permissionEntities = await _userRepository.GetUserPermissionsAsync(userId);
        var permissionNames = permissionEntities.Select(p => p.Name).ToList();

        // Actualizar caché
        var roles = await GetUserRolesInternalAsync(userId);
        _cache[userId] = (roles, permissionNames, DateTime.UtcNow);

        return permissionNames;
    }

    /// <summary>
    /// Obtiene todos los roles de un usuario (con caché)
    /// </summary>
    public async Task<List<string>> GetUserRolesAsync(int userId)
    {
        // Verificar caché
        if (_cache.TryGetValue(userId, out var cached))
        {
            if (DateTime.UtcNow - cached.cachedAt < _cacheExpiration)
            {
                return cached.roles;
            }
            else
            {
                _cache.Remove(userId);
            }
        }

        // Obtener roles desde BD
        var roleNames = await GetUserRolesInternalAsync(userId);

        // Actualizar caché
        var permissions = await GetUserPermissionsInternalAsync(userId);
        _cache[userId] = (roleNames, permissions, DateTime.UtcNow);

        return roleNames;
    }

    /// <summary>
    /// Obtiene roles sin usar caché (interno)
    /// </summary>
    private async Task<List<string>> GetUserRolesInternalAsync(int userId)
    {
        var roleEntities = await _userRepository.GetUserRolesAsync(userId);
        return roleEntities.Select(r => r.Name).ToList();
    }

    /// <summary>
    /// Obtiene permisos sin usar caché (interno)
    /// </summary>
    private async Task<List<string>> GetUserPermissionsInternalAsync(int userId)
    {
        var permissionEntities = await _userRepository.GetUserPermissionsAsync(userId);
        return permissionEntities.Select(p => p.Name).ToList();
    }

    /// <summary>
    /// Invalida el caché de un usuario (útil después de cambios de roles/permisos)
    /// </summary>
    public void InvalidateUserCache(int userId)
    {
        _cache.Remove(userId);
    }

    /// <summary>
    /// Limpia todo el caché
    /// </summary>
    public void ClearCache()
    {
        _cache.Clear();
    }
}
