using MetaForge.Core.Entities.Security;

namespace MetaForge.Core.Repositories;

/// <summary>
/// Repositorio para gestión de roles
/// </summary>
public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(int id);
    Task<Role?> GetByNameAsync(string name);
    Task<List<Role>> GetAllAsync();
    Task<(List<Role> roles, int total)> GetPagedAsync(int skip, int take, string? searchTerm = null);
    Task<Role> CreateAsync(Role role);
    Task<Role> UpdateAsync(Role role);
    Task DeleteAsync(int id);
    Task<bool> NameExistsAsync(string name, int? excludeRoleId = null);
    Task<List<Permission>> GetRolePermissionsAsync(int roleId);
    Task AddPermissionAsync(int roleId, int permissionId);
    Task RemovePermissionAsync(int roleId, int permissionId);
    Task<List<User>> GetUsersInRoleAsync(int roleId);
}
