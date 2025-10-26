namespace MetaForge.Core.Entities.Notification;

/// <summary>
/// Plantilla de email
/// </summary>
public class EmailTemplate
{
    /// <summary>
    /// Identificador único
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre de la plantilla
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Código único de la plantilla
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Asunto del email (puede contener placeholders)
    /// </summary>
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Cuerpo del email en HTML
    /// </summary>
    public string HtmlBody { get; set; } = string.Empty;

    /// <summary>
    /// Cuerpo del email en texto plano
    /// </summary>
    public string? TextBody { get; set; }

    /// <summary>
    /// Email del remitente
    /// </summary>
    public string? FromEmail { get; set; }

    /// <summary>
    /// Nombre del remitente
    /// </summary>
    public string? FromName { get; set; }

    /// <summary>
    /// Indica si la plantilla está activa
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Fecha de creación
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Fecha de última actualización
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
