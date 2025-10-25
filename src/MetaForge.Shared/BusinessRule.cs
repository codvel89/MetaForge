namespace MetaForge.Shared;

/// <summary>
/// Representa una regla de negocio aplicable a una tabla
/// </summary>
public class BusinessRule
{
    /// <summary>
    /// Nombre identificador de la regla
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de regla (Validation, Action, Calculation)
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Momento en que se dispara la regla
    /// </summary>
    public string Trigger { get; set; } = string.Empty;

    /// <summary>
    /// Condición que debe cumplirse para ejecutar la regla
    /// </summary>
    public string? Condition { get; set; }

    /// <summary>
    /// Expresión a evaluar
    /// </summary>
    public string? Expression { get; set; }

    /// <summary>
    /// Mensaje de error si la validación falla
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Lista de acciones a ejecutar
    /// </summary>
    public List<string>? Actions { get; set; }
}
