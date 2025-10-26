namespace MetaForge.Core.Services.Security;

/// <summary>
/// Servicio de autorización y verificación de permisos
/// </summary>
public interface IAuthorizationService
{
    /// <summary>
    /// Verifica si un usuario tiene un permiso específico
    /// </summary>
    /// <param name="userId">ID del usuario</param>
    /// <param name="permission">Nombre del permiso (ej: "users.create")</param>
    /// <returns>True si tiene el permiso</returns>
    Task<bool> HasPermissionAsync(int userId, string permission);

    /// <summary>
    /// Verifica si un usuario tiene un rol específico
    /// </summary>
    /// <param name="userId">ID del usuario</param>
    /// <param name="roleName">Nombre del rol</param>
    /// <returns>True si tiene el rol</returns>
    Task<bool> HasRoleAsync(int userId, string roleName);

    /// <summary>
    /// Verifica si un usuario tiene acceso a un recurso con una acción
    /// </summary>
    /// <param name="userId">ID del usuario</param>
    /// <param name="resource">Recurso (ej: "users")</param>
    /// <param name="action">Acción (ej: "create", "read", "update", "delete")</param>
    /// <returns>True si tiene acceso</returns>
    Task<bool> CanAccessAsync(int userId, string resource, string action);

    /// <summary>
    /// Obtiene todos los permisos de un usuario
    /// </summary>
    /// <param name="userId">ID del usuario</param>
    /// <returns>Lista de nombres de permisos</returns>
    Task<List<string>> GetUserPermissionsAsync(int userId);

    /// <summary>
    /// Obtiene todos los roles de un usuario
    /// </summary>
    /// <param name="userId">ID del usuario</param>
    /// <returns>Lista de nombres de roles</returns>
    Task<List<string>> GetUserRolesAsync(int userId);
}
