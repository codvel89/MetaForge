namespace MetaForge.Core.Entities.Audit;

/// <summary>
/// Registro de auditoría de cambios en el sistema
/// </summary>
public class AuditLog
{
    /// <summary>
    /// Identificador único
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Nombre de la entidad/tabla
    /// </summary>
    public string EntityName { get; set; } = string.Empty;

    /// <summary>
    /// ID del registro afectado
    /// </summary>
    public string EntityId { get; set; } = string.Empty;

    /// <summary>
    /// Acción realizada (Insert, Update, Delete)
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Cambios realizados (JSON con before/after)
    /// </summary>
    public string Changes { get; set; } = string.Empty;

    /// <summary>
    /// Valores anteriores (JSON)
    /// </summary>
    public string? OldValues { get; set; }

    /// <summary>
    /// Valores nuevos (JSON)
    /// </summary>
    public string? NewValues { get; set; }

    /// <summary>
    /// Usuario que realizó la acción
    /// </summary>
    public string? PerformedBy { get; set; }

    /// <summary>
    /// ID del usuario que realizó la acción
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// Nombre del usuario
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Fecha y hora de la acción
    /// </summary>
    public DateTime PerformedAt { get; set; }

    /// <summary>
    /// Fecha y hora de la acción (para compatibilidad)
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// IP del cliente
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// User Agent
    /// </summary>
    public string? UserAgent { get; set; }
}
