using MetaForge.Core.Context;
using MetaForge.Core.Entities.Security;
using Microsoft.EntityFrameworkCore;

namespace MetaForge.Core.Repositories;

public class PermissionRepository : IPermissionRepository
{
    private readonly MetadataDbContext _context;

    public PermissionRepository(MetadataDbContext context)
    {
        _context = context;
    }

    public async Task<Permission?> GetByIdAsync(int id)
    {
        return await _context.Permissions.FindAsync(id);
    }

    public async Task<Permission?> GetByNameAsync(string name)
    {
        return await _context.Permissions
            .FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<Permission?> GetByResourceAndActionAsync(string resource, string action)
    {
        return await _context.Permissions
            .FirstOrDefaultAsync(p => p.Resource == resource && p.Action == action);
    }

    public async Task<List<Permission>> GetAllAsync()
    {
        return await _context.Permissions
            .OrderBy(p => p.Category)
            .ThenBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<List<Permission>> GetByCategoryAsync(string category)
    {
        return await _context.Permissions
            .Where(p => p.Category == category)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<(List<Permission> permissions, int total)> GetPagedAsync(int skip, int take, string? searchTerm = null, string? category = null)
    {
        var query = _context.Permissions.AsQueryable();

        if (!string.IsNullOrWhiteSpace(category))
            query = query.Where(p => p.Category == category);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(p =>
                p.Name.Contains(searchTerm) ||
                p.Resource.Contains(searchTerm) ||
                (p.Description != null && p.Description.Contains(searchTerm))
            );
        }

        var total = await query.CountAsync();
        var permissions = await query
            .OrderBy(p => p.Category)
            .ThenBy(p => p.Name)
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        return (permissions, total);
    }

    public async Task<Permission> CreateAsync(Permission permission)
    {
        _context.Permissions.Add(permission);
        await _context.SaveChangesAsync();
        return permission;
    }

    public async Task<Permission> UpdateAsync(Permission permission)
    {
        _context.Permissions.Update(permission);
        await _context.SaveChangesAsync();
        return permission;
    }

    public async Task DeleteAsync(int id)
    {
        var permission = await _context.Permissions.FindAsync(id);
        if (permission != null)
        {
            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> NameExistsAsync(string name, int? excludePermissionId = null)
    {
        var query = _context.Permissions.Where(p => p.Name == name);

        if (excludePermissionId.HasValue)
            query = query.Where(p => p.Id != excludePermissionId.Value);

        return await query.AnyAsync();
    }

    public async Task<bool> ResourceActionExistsAsync(string resource, string action, int? excludePermissionId = null)
    {
        var query = _context.Permissions.Where(p => p.Resource == resource && p.Action == action);

        if (excludePermissionId.HasValue)
            query = query.Where(p => p.Id != excludePermissionId.Value);

        return await query.AnyAsync();
    }
}
