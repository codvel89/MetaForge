namespace MetaForge.Core.Messaging.Messages;

/// <summary>
/// Mensaje para encolar el envío de una notificación
/// </summary>
public class NotificationQueuedMessage
{
    /// <summary>
    /// Código de la plantilla de notificación a utilizar
    /// </summary>
    public required string TemplateCode { get; set; }

    /// <summary>
    /// Identificador del usuario destinatario
    /// </summary>
    public required int UserId { get; set; }

    /// <summary>
    /// Canales de entrega (email, sms, push, etc.)
    /// </summary>
    public required List<string> Channels { get; set; }

    /// <summary>
    /// Variables para reemplazar en la plantilla
    /// </summary>
    public Dictionary<string, string> Variables { get; set; } = new();

    /// <summary>
    /// Datos adicionales para personalización del canal
    /// </summary>
    public Dictionary<string, object>? Metadata { get; set; }

    /// <summary>
    /// Prioridad del mensaje (1=alta, 5=normal, 10=baja)
    /// </summary>
    public int Priority { get; set; } = 5;

    /// <summary>
    /// Identificador de correlación para auditoría
    /// </summary>
    public string? CorrelationId { get; set; }

    /// <summary>
    /// Fecha y hora de expiración del mensaje (opcional)
    /// </summary>
    public DateTime? ExpiresAt { get; set; }
}
