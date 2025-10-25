namespace MetaForge.Shared;

/// <summary>
/// Define un filtro rápido para vistas de lista
/// </summary>
public class QuickFilter
{
    /// <summary>
    /// Etiqueta visible del filtro
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Expresión de filtro a aplicar
    /// </summary>
    public string Expression { get; set; } = string.Empty;

    /// <summary>
    /// Icono asociado al filtro
    /// </summary>
    public string? Icon { get; set; }
}
