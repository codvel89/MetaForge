using MetaForge.Core.Entities.Security;

namespace MetaForge.Core.Repositories;

/// <summary>
/// Repositorio para gestión de permisos
/// </summary>
public interface IPermissionRepository
{
    Task<Permission?> GetByIdAsync(int id);
    Task<Permission?> GetByNameAsync(string name);
    Task<Permission?> GetByResourceAndActionAsync(string resource, string action);
    Task<List<Permission>> GetAllAsync();
    Task<List<Permission>> GetByCategoryAsync(string category);
    Task<(List<Permission> permissions, int total)> GetPagedAsync(int skip, int take, string? searchTerm = null, string? category = null);
    Task<Permission> CreateAsync(Permission permission);
    Task<Permission> UpdateAsync(Permission permission);
    Task DeleteAsync(int id);
    Task<bool> NameExistsAsync(string name, int? excludePermissionId = null);
    Task<bool> ResourceActionExistsAsync(string resource, string action, int? excludePermissionId = null);
}
