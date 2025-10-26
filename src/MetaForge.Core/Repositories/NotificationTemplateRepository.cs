using MetaForge.Core.Context;
using MetaForge.Core.Entities.Notification;
using Microsoft.EntityFrameworkCore;

namespace MetaForge.Core.Repositories;

public class NotificationTemplateRepository : INotificationTemplateRepository
{
    private readonly MetadataDbContext _context;

    public NotificationTemplateRepository(MetadataDbContext context)
    {
        _context = context;
    }

    public async Task<NotificationTemplate?> GetByIdAsync(int id)
    {
        return await _context.NotificationTemplates.FindAsync(id);
    }

    public async Task<NotificationTemplate?> GetByCodeAsync(string code)
    {
        return await _context.NotificationTemplates
            .FirstOrDefaultAsync(t => t.Code == code);
    }

    public async Task<List<NotificationTemplate>> GetAllAsync(bool activeOnly = false)
    {
        var query = _context.NotificationTemplates.AsQueryable();

        if (activeOnly)
            query = query.Where(t => t.IsActive);

        return await query.OrderBy(t => t.Name).ToListAsync();
    }

    public async Task<List<NotificationTemplate>> GetByCategoryAsync(string category)
    {
        return await _context.NotificationTemplates
            .Where(t => t.Category == category)
            .OrderBy(t => t.Name)
            .ToListAsync();
    }

    public async Task<NotificationTemplate> CreateAsync(NotificationTemplate template)
    {
        template.CreatedAt = DateTime.UtcNow;
        _context.NotificationTemplates.Add(template);
        await _context.SaveChangesAsync();
        return template;
    }

    public async Task<NotificationTemplate> UpdateAsync(NotificationTemplate template)
    {
        template.UpdatedAt = DateTime.UtcNow;
        _context.NotificationTemplates.Update(template);
        await _context.SaveChangesAsync();
        return template;
    }

    public async Task DeleteAsync(int id)
    {
        var template = await _context.NotificationTemplates.FindAsync(id);
        if (template != null)
        {
            _context.NotificationTemplates.Remove(template);
            await _context.SaveChangesAsync();
        }
    }
}

public class EmailTemplateRepository : IEmailTemplateRepository
{
    private readonly MetadataDbContext _context;

    public EmailTemplateRepository(MetadataDbContext context)
    {
        _context = context;
    }

    public async Task<EmailTemplate?> GetByIdAsync(int id)
    {
        return await _context.EmailTemplates.FindAsync(id);
    }

    public async Task<EmailTemplate?> GetByCodeAsync(string code)
    {
        return await _context.EmailTemplates
            .FirstOrDefaultAsync(t => t.Code == code);
    }

    public async Task<List<EmailTemplate>> GetAllAsync(bool activeOnly = false)
    {
        var query = _context.EmailTemplates.AsQueryable();

        if (activeOnly)
            query = query.Where(t => t.IsActive);

        return await query.OrderBy(t => t.Name).ToListAsync();
    }

    public async Task<List<EmailTemplate>> GetByCategoryAsync(string category)
    {
        return await _context.EmailTemplates
            .Where(t => t.Category == category)
            .OrderBy(t => t.Name)
            .ToListAsync();
    }

    public async Task<EmailTemplate> CreateAsync(EmailTemplate template)
    {
        template.CreatedAt = DateTime.UtcNow;
        _context.EmailTemplates.Add(template);
        await _context.SaveChangesAsync();
        return template;
    }

    public async Task<EmailTemplate> UpdateAsync(EmailTemplate template)
    {
        template.UpdatedAt = DateTime.UtcNow;
        _context.EmailTemplates.Update(template);
        await _context.SaveChangesAsync();
        return template;
    }

    public async Task DeleteAsync(int id)
    {
        var template = await _context.EmailTemplates.FindAsync(id);
        if (template != null)
        {
            _context.EmailTemplates.Remove(template);
            await _context.SaveChangesAsync();
        }
    }
}
