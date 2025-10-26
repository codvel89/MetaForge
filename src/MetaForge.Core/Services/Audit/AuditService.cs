using MetaForge.Core.Context;
using MetaForge.Core.Entities.Audit;
using Microsoft.EntityFrameworkCore;

namespace MetaForge.Core.Services.Audit;

/// <summary>
/// Implementación del servicio de auditoría
/// </summary>
public class AuditService : IAuditService
{
    private readonly MetadataDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    public AuditService(MetadataDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Registra una operación de creación
    /// </summary>
    public async Task LogCreateAsync(string entityName, string entityId, string newValues, string? performedBy = null, string? ipAddress = null, string? userAgent = null)
    {
        var auditLog = new AuditLog
        {
            EntityName = entityName,
            EntityId = entityId,
            Action = "Create",
            Changes = newValues,
            NewValues = newValues,
            PerformedBy = performedBy,
            PerformedAt = DateTime.UtcNow,
            Timestamp = DateTime.UtcNow,
            IpAddress = ipAddress,
            UserAgent = userAgent
        };

        _context.AuditLogs.Add(auditLog);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Registra una operación de actualización
    /// </summary>
    public async Task LogUpdateAsync(string entityName, string entityId, string oldValues, string newValues, string? performedBy = null, string? ipAddress = null, string? userAgent = null)
    {
        var auditLog = new AuditLog
        {
            EntityName = entityName,
            EntityId = entityId,
            Action = "Update",
            Changes = $"{{\"old\":{oldValues},\"new\":{newValues}}}",
            OldValues = oldValues,
            NewValues = newValues,
            PerformedBy = performedBy,
            PerformedAt = DateTime.UtcNow,
            Timestamp = DateTime.UtcNow,
            IpAddress = ipAddress,
            UserAgent = userAgent
        };

        _context.AuditLogs.Add(auditLog);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Registra una operación de eliminación
    /// </summary>
    public async Task LogDeleteAsync(string entityName, string entityId, string oldValues, string? performedBy = null, string? ipAddress = null, string? userAgent = null)
    {
        var auditLog = new AuditLog
        {
            EntityName = entityName,
            EntityId = entityId,
            Action = "Delete",
            Changes = oldValues,
            OldValues = oldValues,
            PerformedBy = performedBy,
            PerformedAt = DateTime.UtcNow,
            Timestamp = DateTime.UtcNow,
            IpAddress = ipAddress,
            UserAgent = userAgent
        };

        _context.AuditLogs.Add(auditLog);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Obtiene el historial de auditoría de una entidad
    /// </summary>
    public async Task<List<AuditLog>> GetEntityHistoryAsync(string entityName, string entityId, int skip = 0, int take = 50)
    {
        return await _context.AuditLogs
            .Where(a => a.EntityName == entityName && a.EntityId == entityId)
            .OrderByDescending(a => a.PerformedAt)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    /// <summary>
    /// Obtiene el historial de auditoría por usuario
    /// </summary>
    public async Task<List<AuditLog>> GetUserHistoryAsync(string performedBy, int skip = 0, int take = 50)
    {
        return await _context.AuditLogs
            .Where(a => a.PerformedBy == performedBy)
            .OrderByDescending(a => a.PerformedAt)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }
}
