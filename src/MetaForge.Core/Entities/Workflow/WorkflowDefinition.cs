namespace MetaForge.Core.Entities.Workflow;

/// <summary>
/// Definición de un workflow
/// </summary>
public class WorkflowDefinition
{
    /// <summary>
    /// Identificador único
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre del workflow
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Código único del workflow
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del workflow
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Evento que dispara el workflow
    /// </summary>
    public string TriggerEvent { get; set; } = string.Empty;

    /// <summary>
    /// Condición para ejecutar el workflow (expresión)
    /// </summary>
    public string? Condition { get; set; }

    /// <summary>
    /// Definición de pasos en JSON
    /// </summary>
    public string StepsDefinition { get; set; } = string.Empty;

    /// <summary>
    /// Definición completa del workflow en JSON (alias)
    /// </summary>
    public string Definition
    {
        get => StepsDefinition;
        set => StepsDefinition = value;
    }

    /// <summary>
    /// Categoría del workflow
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Indica si el workflow está activo
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Versión del workflow
    /// </summary>
    public string Version { get; set; } = "1.0";

    /// <summary>
    /// Fecha de creación
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Fecha de última actualización
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Instancias del workflow
    /// </summary>
    public List<WorkflowInstance> Instances { get; set; } = new();
}
