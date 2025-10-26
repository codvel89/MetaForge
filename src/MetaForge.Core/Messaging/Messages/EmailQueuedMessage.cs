namespace MetaForge.Core.Messaging.Messages;

/// <summary>
/// Mensaje para encolar el envío de un correo electrónico
/// </summary>
public class EmailQueuedMessage
{
    /// <summary>
    /// Código de la plantilla de email a utilizar
    /// </summary>
    public required string TemplateCode { get; set; }

    /// <summary>
    /// Dirección de correo del destinatario
    /// </summary>
    public required string To { get; set; }

    /// <summary>
    /// Nombre del destinatario (opcional)
    /// </summary>
    public string? ToName { get; set; }

    /// <summary>
    /// Dirección de correo del remitente (opcional, usa default del template si no se especifica)
    /// </summary>
    public string? From { get; set; }

    /// <summary>
    /// Nombre del remitente (opcional)
    /// </summary>
    public string? FromName { get; set; }

    /// <summary>
    /// Variables para reemplazar en la plantilla
    /// </summary>
    public Dictionary<string, string> Variables { get; set; } = new();

    /// <summary>
    /// Lista de archivos adjuntos (rutas absolutas)
    /// </summary>
    public List<string>? Attachments { get; set; }

    /// <summary>
    /// Prioridad del mensaje (1=alta, 5=normal, 10=baja)
    /// </summary>
    public int Priority { get; set; } = 5;

    /// <summary>
    /// Identificador de correlación para auditoría
    /// </summary>
    public string? CorrelationId { get; set; }
}
