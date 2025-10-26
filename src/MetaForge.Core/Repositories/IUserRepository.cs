using MetaForge.Core.Entities.Security;

namespace MetaForge.Core.Repositories;

/// <summary>
/// Repositorio para gestión de usuarios
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Obtiene un usuario por ID
    /// </summary>
    Task<User?> GetByIdAsync(int id);

    /// <summary>
    /// Obtiene un usuario por username
    /// </summary>
    Task<User?> GetByUsernameAsync(string username);

    /// <summary>
    /// Obtiene un usuario por email
    /// </summary>
    Task<User?> GetByEmailAsync(string email);

    /// <summary>
    /// Obtiene un usuario por username o email
    /// </summary>
    Task<User?> GetByUsernameOrEmailAsync(string usernameOrEmail);

    /// <summary>
    /// Obtiene todos los usuarios
    /// </summary>
    Task<List<User>> GetAllAsync(bool includeInactive = false);

    /// <summary>
    /// Obtiene usuarios con paginación
    /// </summary>
    Task<(List<User> users, int total)> GetPagedAsync(int skip, int take, string? searchTerm = null, bool includeInactive = false);

    /// <summary>
    /// Crea un nuevo usuario
    /// </summary>
    Task<User> CreateAsync(User user);

    /// <summary>
    /// Actualiza un usuario
    /// </summary>
    Task<User> UpdateAsync(User user);

    /// <summary>
    /// Elimina un usuario
    /// </summary>
    Task DeleteAsync(int id);

    /// <summary>
    /// Verifica si un username ya existe
    /// </summary>
    Task<bool> UsernameExistsAsync(string username, int? excludeUserId = null);

    /// <summary>
    /// Verifica si un email ya existe
    /// </summary>
    Task<bool> EmailExistsAsync(string email, int? excludeUserId = null);

    /// <summary>
    /// Obtiene los roles de un usuario
    /// </summary>
    Task<List<Role>> GetUserRolesAsync(int userId);

    /// <summary>
    /// Obtiene los permisos de un usuario (desde sus roles)
    /// </summary>
    Task<List<Permission>> GetUserPermissionsAsync(int userId);

    /// <summary>
    /// Actualiza el último login de un usuario
    /// </summary>
    Task UpdateLastLoginAsync(int userId);

    /// <summary>
    /// Actualiza el refresh token de un usuario
    /// </summary>
    Task UpdateRefreshTokenAsync(int userId, string refreshToken, DateTime expiry);
}
