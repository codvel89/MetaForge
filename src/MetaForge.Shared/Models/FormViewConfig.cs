namespace MetaForge.Shared.Models;

/// <summary>
/// Configuración de la vista de formulario para una tabla
/// </summary>
public class FormViewConfig
{
    /// <summary>
    /// Layout del formulario (OneColumn, TwoColumns, ThreeColumns)
    /// </summary>
    public string Layout { get; set; } = "TwoColumns";

    /// <summary>
    /// Secciones del formulario
    /// </summary>
    public List<FormSection> Sections { get; set; } = new();

    /// <summary>
    /// Indica si se muestran tabs para las secciones
    /// </summary>
    public bool UseTabs { get; set; }

    /// <summary>
    /// Ancho del formulario (small, medium, large, full)
    /// </summary>
    public string? Width { get; set; }
}
