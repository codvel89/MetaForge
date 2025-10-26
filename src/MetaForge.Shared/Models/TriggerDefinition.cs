namespace MetaForge.Shared.Models;

/// <summary>
/// Define un trigger (disparador) para una tabla
/// </summary>
public class TriggerDefinition
{
    /// <summary>
    /// Nombre del trigger
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Evento que dispara el trigger (BeforeInsert, AfterInsert, BeforeUpdate, AfterUpdate, BeforeDelete, AfterDelete)
    /// </summary>
    public string Event { get; set; } = string.Empty;

    /// <summary>
    /// Condición opcional que debe cumplirse para ejecutar el trigger
    /// </summary>
    public string? Condition { get; set; }

    /// <summary>
    /// Lista de acciones a ejecutar cuando se dispara el trigger
    /// </summary>
    public List<TriggerAction> Actions { get; set; } = new();
}
