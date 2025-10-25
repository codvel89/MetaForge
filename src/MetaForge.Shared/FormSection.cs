namespace MetaForge.Shared;

/// <summary>
/// Representa una sección dentro de un formulario
/// </summary>
public class FormSection
{
    /// <summary>
    /// Título de la sección
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Indica si la sección ocupa todo el ancho
    /// </summary>
    public bool FullWidth { get; set; }

    /// <summary>
    /// Lista de columnas (campos) en la sección
    /// </summary>
    public string[] Columns { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Indica si la sección está colapsada por defecto
    /// </summary>
    public bool Collapsed { get; set; }
}
