namespace MetaForge.Shared.Models;

/// <summary>
/// Define un campo calculado dinámicamente
/// </summary>
public class ComputedFieldDefinition
{
    /// <summary>
    /// Nombre del campo calculado
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Nombre visible del campo
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// Expresión para calcular el valor
    /// </summary>
    public string Expression { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de dato del resultado
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Indica si se muestra en listas
    /// </summary>
    public bool ShowInList { get; set; }

    /// <summary>
    /// Formato de presentación del valor
    /// </summary>
    public string? Format { get; set; }
}
