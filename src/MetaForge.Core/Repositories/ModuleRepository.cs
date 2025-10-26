using MetaForge.Core.Context;
using MetaForge.Core.Entities.System;
using Microsoft.EntityFrameworkCore;

namespace MetaForge.Core.Repositories;

public class ModuleRepository : IModuleRepository
{
    private readonly MetadataDbContext _context;

    public ModuleRepository(MetadataDbContext context)
    {
        _context = context;
    }

    public async Task<Module?> GetByIdAsync(int id)
    {
        return await _context.Modules
            .Include(m => m.Dependencies)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Module?> GetByNameAsync(string name)
    {
        return await _context.Modules
            .Include(m => m.Dependencies)
            .FirstOrDefaultAsync(m => m.Name == name);
    }

    public async Task<List<Module>> GetAllAsync(bool installedOnly = false)
    {
        var query = _context.Modules.AsQueryable();

        if (installedOnly)
            query = query.Where(m => m.IsInstalled);

        return await query
            .OrderBy(m => m.Name)
            .ToListAsync();
    }

    public async Task<List<Module>> GetInstalledAsync()
    {
        return await _context.Modules
            .Where(m => m.IsInstalled)
            .OrderBy(m => m.Name)
            .ToListAsync();
    }

    public async Task<List<Module>> GetActiveAsync()
    {
        return await _context.Modules
            .Where(m => m.IsInstalled && m.IsActive)
            .OrderBy(m => m.Name)
            .ToListAsync();
    }

    public async Task<Module> CreateAsync(Module module)
    {
        _context.Modules.Add(module);
        await _context.SaveChangesAsync();
        return module;
    }

    public async Task<Module> UpdateAsync(Module module)
    {
        _context.Modules.Update(module);
        await _context.SaveChangesAsync();
        return module;
    }

    public async Task DeleteAsync(int id)
    {
        var module = await _context.Modules.FindAsync(id);
        if (module != null)
        {
            _context.Modules.Remove(module);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> NameExistsAsync(string name, int? excludeModuleId = null)
    {
        var query = _context.Modules.Where(m => m.Name == name);

        if (excludeModuleId.HasValue)
            query = query.Where(m => m.Id != excludeModuleId.Value);

        return await query.AnyAsync();
    }

    public async Task<List<ModuleDependency>> GetDependenciesAsync(int moduleId)
    {
        return await _context.ModuleDependencies
            .Where(md => md.ModuleId == moduleId)
            .ToListAsync();
    }

    public async Task<bool> HasDependentsAsync(int moduleId)
    {
        var moduleName = await _context.Modules
            .Where(m => m.Id == moduleId)
            .Select(m => m.Name)
            .FirstOrDefaultAsync();

        if (string.IsNullOrEmpty(moduleName))
            return false;

        return await _context.ModuleDependencies
            .AnyAsync(md => md.RequiredModuleName == moduleName);
    }
}
