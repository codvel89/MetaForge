namespace MetaForge.Core.Messaging.Messages;

/// <summary>
/// Mensaje para disparar la ejecución de un workflow
/// </summary>
public class WorkflowTriggeredMessage
{
    /// <summary>
    /// Identificador de la definición del workflow
    /// </summary>
    public required int WorkflowDefinitionId { get; set; }

    /// <summary>
    /// Identificador del usuario que dispara el workflow (opcional)
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// Tipo de entidad que dispara el workflow
    /// </summary>
    public required string EntityType { get; set; }

    /// <summary>
    /// Identificador de la entidad que dispara el workflow
    /// </summary>
    public required int EntityId { get; set; }

    /// <summary>
    /// Evento que disparó el workflow (create, update, delete, custom)
    /// </summary>
    public required string TriggerEvent { get; set; }

    /// <summary>
    /// Datos de contexto para el workflow
    /// </summary>
    public Dictionary<string, object> Context { get; set; } = new();

    /// <summary>
    /// Variables iniciales para el workflow
    /// </summary>
    public Dictionary<string, object>? Variables { get; set; }

    /// <summary>
    /// Prioridad del mensaje (1=alta, 5=normal, 10=baja)
    /// </summary>
    public int Priority { get; set; } = 5;

    /// <summary>
    /// Identificador de correlación para auditoría
    /// </summary>
    public string? CorrelationId { get; set; }

    /// <summary>
    /// Fecha y hora en que se disparó el evento
    /// </summary>
    public DateTime TriggeredAt { get; set; } = DateTime.UtcNow;
}
