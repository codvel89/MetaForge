using MetaForge.Core.Entities.System;

namespace MetaForge.Core.Repositories;

/// <summary>
/// Repositorio para gestión de módulos
/// </summary>
public interface IModuleRepository
{
    Task<Module?> GetByIdAsync(int id);
    Task<Module?> GetByNameAsync(string name);
    Task<List<Module>> GetAllAsync(bool installedOnly = false);
    Task<List<Module>> GetInstalledAsync();
    Task<List<Module>> GetActiveAsync();
    Task<Module> CreateAsync(Module module);
    Task<Module> UpdateAsync(Module module);
    Task DeleteAsync(int id);
    Task<bool> NameExistsAsync(string name, int? excludeModuleId = null);
    Task<List<ModuleDependency>> GetDependenciesAsync(int moduleId);
    Task<bool> HasDependentsAsync(int moduleId);
}
