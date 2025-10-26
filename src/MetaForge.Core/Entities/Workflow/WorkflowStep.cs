namespace MetaForge.Core.Entities.Workflow;

/// <summary>
/// Paso ejecutado de un workflow
/// </summary>
public class WorkflowStep
{
    /// <summary>
    /// Identificador único
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID de la instancia del workflow
    /// </summary>
    public int WorkflowInstanceId { get; set; }

    /// <summary>
    /// Número del paso
    /// </summary>
    public int StepNumber { get; set; }

    /// <summary>
    /// Nombre del paso
    /// </summary>
    public string StepName { get; set; } = string.Empty;

    /// <summary>
    /// Estado del paso (Pending, Running, Completed, Failed, Skipped)
    /// </summary>
    public string Status { get; set; } = "Pending";

    /// <summary>
    /// Input del paso en JSON
    /// </summary>
    public string? Input { get; set; }

    /// <summary>
    /// Output del paso en JSON
    /// </summary>
    public string? Output { get; set; }

    /// <summary>
    /// Mensaje de error si falló
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Fecha de inicio
    /// </summary>
    public DateTime StartedAt { get; set; }

    /// <summary>
    /// Fecha de finalización
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Instancia del workflow
    /// </summary>
    public WorkflowInstance WorkflowInstance { get; set; } = null!;
}
