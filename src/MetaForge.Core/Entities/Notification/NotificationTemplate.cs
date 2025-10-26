namespace MetaForge.Core.Entities.Notification;

/// <summary>
/// Plantilla de notificación
/// </summary>
public class NotificationTemplate
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
    /// Tipo de notificación (Push, WebSocket, SMS)
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Asunto/Título de la notificación
    /// </summary>
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Cuerpo del mensaje (puede contener placeholders)
    /// </summary>
    public string Body { get; set; } = string.Empty;

    /// <summary>
    /// Datos adicionales en JSON (iconos, colores, etc.)
    /// </summary>
    public string? Metadata { get; set; }

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
