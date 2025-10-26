namespace MetaForge.Core.Entities.Workflow;

/// <summary>
/// Instancia en ejecución de un workflow
/// </summary>
public class WorkflowInstance
{
    /// <summary>
    /// Identificador único
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID del workflow definición
    /// </summary>
    public int WorkflowDefinitionId { get; set; }

    /// <summary>
    /// Estado actual (Pending, Running, Completed, Failed, Cancelled)
    /// </summary>
    public string Status { get; set; } = "Pending";

    /// <summary>
    /// Datos de contexto en JSON
    /// </summary>
    public string? ContextData { get; set; }

    /// <summary>
    /// Paso actual en ejecución
    /// </summary>
    public int CurrentStep { get; set; }

    /// <summary>
    /// Fecha de inicio
    /// </summary>
    public DateTime StartedAt { get; set; }

    /// <summary>
    /// Fecha de finalización
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Mensaje de error si falló
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Usuario que inició el workflow
    /// </summary>
    public int? StartedBy { get; set; }

    /// <summary>
    /// Workflow definición
    /// </summary>
    public WorkflowDefinition WorkflowDefinition { get; set; } = null!;

    /// <summary>
    /// Pasos ejecutados
    /// </summary>
    public List<WorkflowStep> Steps { get; set; } = new();
}
