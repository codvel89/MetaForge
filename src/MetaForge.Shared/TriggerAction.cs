namespace MetaForge.Shared;

/// <summary>
/// Representa una acción a ejecutar en un trigger
/// </summary>
public class TriggerAction
{
    /// <summary>
    /// Tipo de acción a ejecutar
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Campo sobre el que se ejecuta la acción
    /// </summary>
    public string? Field { get; set; }

    /// <summary>
    /// Valor a asignar
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// Plantilla para notificaciones
    /// </summary>
    public string? Template { get; set; }

    /// <summary>
    /// Lista de destinatarios para notificaciones
    /// </summary>
    public string[]? Recipients { get; set; }

    /// <summary>
    /// Identificador de workflow a ejecutar
    /// </summary>
    public string? WorkflowId { get; set; }

    /// <summary>
    /// Condición adicional para ejecutar la acción
    /// </summary>
    public string? Condition { get; set; }
}
