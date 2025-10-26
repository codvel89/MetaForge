using MetaForge.Core.Context;
using MetaForge.Core.Entities.Notification;
using Microsoft.EntityFrameworkCore;

namespace MetaForge.Core.Repositories;

/// <summary>
/// Implementación del repositorio para gestionar plantillas de correo electrónico
/// </summary>
public class EmailTemplateRepository : IEmailTemplateRepository
{
    private readonly MetadataDbContext _context;

    public EmailTemplateRepository(MetadataDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene una plantilla por su identificador
    /// </summary>
    public async Task<EmailTemplate?> GetByIdAsync(int id)
    {
        return await _context.EmailTemplates.FindAsync(id);
    }

    /// <summary>
    /// Obtiene una plantilla por su código único
    /// </summary>
    public async Task<EmailTemplate?> GetByCodeAsync(string code)
    {
        return await _context.EmailTemplates
            .FirstOrDefaultAsync(t => t.Code == code);
    }

    /// <summary>
    /// Obtiene todas las plantillas, opcionalmente solo las activas
    /// </summary>
    public async Task<List<EmailTemplate>> GetAllAsync(bool activeOnly = false)
    {
        var query = _context.EmailTemplates.AsQueryable();

        if (activeOnly)
            query = query.Where(t => t.IsActive);

        return await query.OrderBy(t => t.Name).ToListAsync();
    }

    /// <summary>
    /// Obtiene plantillas por categoría
    /// </summary>
    public async Task<List<EmailTemplate>> GetByCategoryAsync(string category)
    {
        return await _context.EmailTemplates
            .Where(t => t.Category == category)
            .OrderBy(t => t.Name)
            .ToListAsync();
    }

    /// <summary>
    /// Crea una nueva plantilla
    /// </summary>
    public async Task<EmailTemplate> CreateAsync(EmailTemplate template)
    {
        template.CreatedAt = DateTime.UtcNow;
        _context.EmailTemplates.Add(template);
        await _context.SaveChangesAsync();
        return template;
    }

    /// <summary>
    /// Actualiza una plantilla existente
    /// </summary>
    public async Task<EmailTemplate> UpdateAsync(EmailTemplate template)
    {
        template.UpdatedAt = DateTime.UtcNow;
        _context.EmailTemplates.Update(template);
        await _context.SaveChangesAsync();
        return template;
    }

    /// <summary>
    /// Elimina una plantilla por su identificador
    /// </summary>
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
