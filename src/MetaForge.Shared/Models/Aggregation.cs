namespace MetaForge.Shared.Models;

/// <summary>
/// Define una agregación (suma, promedio, etc.) para una vista de lista
/// </summary>
public class Aggregation
{
    /// <summary>
    /// Campo sobre el que se aplica la agregación
    /// </summary>
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// Función de agregación (Sum, Avg, Count, Min, Max)
    /// </summary>
    public string Function { get; set; } = string.Empty;

    /// <summary>
    /// Etiqueta visible del resultado
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Formato de presentación del resultado
    /// </summary>
    public string? Format { get; set; }
}
