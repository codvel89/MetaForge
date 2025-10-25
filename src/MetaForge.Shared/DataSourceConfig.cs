namespace MetaForge.Shared;

/// <summary>
/// Configuración de fuente de datos para controles dinámicos
/// </summary>
public class DataSourceConfig
{
    /// <summary>
    /// Tipo de fuente de datos (Table, API, Static)
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Nombre de la tabla cuando Type es "Table"
    /// </summary>
    public string? TableName { get; set; }

    /// <summary>
    /// Campo que contiene el valor
    /// </summary>
    public string? ValueField { get; set; }

    /// <summary>
    /// Campo que contiene la etiqueta visible
    /// </summary>
    public string? LabelField { get; set; }

    /// <summary>
    /// Campos en los que se realiza la búsqueda
    /// </summary>
    public string[]? SearchFields { get; set; }

    /// <summary>
    /// Expresión de filtro para limitar los datos
    /// </summary>
    public string? FilterExpression { get; set; }

    /// <summary>
    /// Orden de los resultados
    /// </summary>
    public string? OrderBy { get; set; }
}
