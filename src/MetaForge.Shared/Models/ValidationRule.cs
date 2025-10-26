namespace MetaForge.Shared.Models;

/// <summary>
/// Representa una regla de validación para un campo
/// </summary>
public class ValidationRule
{
    /// <summary>
    /// Tipo de validación (Regex, Email, Range, Custom, ConditionalRegex)
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Patrón de expresión regular para validaciones tipo Regex
    /// </summary>
    public string? Pattern { get; set; }

    /// <summary>
    /// Expresión personalizada para validaciones tipo Custom
    /// </summary>
    public string? Expression { get; set; }

    /// <summary>
    /// Valor mínimo para validaciones tipo Range
    /// </summary>
    public object? Min { get; set; }

    /// <summary>
    /// Valor máximo para validaciones tipo Range
    /// </summary>
    public object? Max { get; set; }

    /// <summary>
    /// Mensaje de error a mostrar cuando falla la validación
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Condiciones para validaciones condicionales
    /// </summary>
    public Dictionary<string, string>? Conditions { get; set; }
}
