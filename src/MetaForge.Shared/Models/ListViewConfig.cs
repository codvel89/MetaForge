namespace MetaForge.Shared.Models;

/// <summary>
/// Configuración de la vista de lista para una tabla
/// </summary>
public class ListViewConfig
{
    /// <summary>
    /// Tamaño de página predeterminado
    /// </summary>
    public int DefaultPageSize { get; set; } = 25;

    /// <summary>
    /// Columna de ordenamiento predeterminada
    /// </summary>
    public string? DefaultSortColumn { get; set; }

    /// <summary>
    /// Orden predeterminado (ASC, DESC)
    /// </summary>
    public string DefaultSortOrder { get; set; } = "ASC";

    /// <summary>
    /// Habilita búsqueda en la lista
    /// </summary>
    public bool EnableSearch { get; set; }

    /// <summary>
    /// Columnas en las que se realiza la búsqueda
    /// </summary>
    public string[]? SearchColumns { get; set; }

    /// <summary>
    /// Habilita filtros
    /// </summary>
    public bool EnableFilters { get; set; }

    /// <summary>
    /// Filtros rápidos predefinidos
    /// </summary>
    public List<QuickFilter>? QuickFilters { get; set; }

    /// <summary>
    /// Campos por los que se puede agrupar
    /// </summary>
    public string[]? GroupBy { get; set; }

    /// <summary>
    /// Agregaciones a mostrar en la lista
    /// </summary>
    public List<Aggregation>? Aggregations { get; set; }

    /// <summary>
    /// Habilita exportación de datos
    /// </summary>
    public bool EnableExport { get; set; }

    /// <summary>
    /// Formatos de exportación permitidos (CSV, Excel, PDF)
    /// </summary>
    public string[]? ExportFormats { get; set; }
}
