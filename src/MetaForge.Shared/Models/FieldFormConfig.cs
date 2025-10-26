namespace MetaForge.Shared.Models;

/// <summary>
/// Configuración de presentación de un campo en formularios
/// </summary>
public class FieldFormConfig
{
    /// <summary>
    /// Componente de UI a utilizar (Input, Select, TextArea, DatePicker, etc.)
    /// </summary>
    public string Component { get; set; } = string.Empty;

    /// <summary>
    /// Texto placeholder del campo
    /// </summary>
    public string? Placeholder { get; set; }

    /// <summary>
    /// Texto de ayuda adicional
    /// </summary>
    public string? HelpText { get; set; }

    /// <summary>
    /// Tipo de input HTML (text, email, password, etc.)
    /// </summary>
    public string? InputType { get; set; }

    /// <summary>
    /// Indica si el campo ocupa todo el ancho disponible
    /// </summary>
    public bool FullWidth { get; set; }

    /// <summary>
    /// Número de filas para TextArea
    /// </summary>
    public int? Rows { get; set; }

    /// <summary>
    /// Opciones estáticas para Select, Radio, etc.
    /// </summary>
    public List<SelectOption>? Options { get; set; }

    /// <summary>
    /// Configuración de fuente de datos dinámica
    /// </summary>
    public DataSourceConfig? DataSource { get; set; }

    /// <summary>
    /// Permite búsqueda en selects
    /// </summary>
    public bool AllowSearch { get; set; }

    /// <summary>
    /// Longitud mínima para iniciar búsqueda
    /// </summary>
    public int? MinSearchLength { get; set; }

    /// <summary>
    /// Formatos aceptados para archivos
    /// </summary>
    public string[]? AcceptedFormats { get; set; }

    /// <summary>
    /// Tamaño máximo de archivo en MB
    /// </summary>
    public int? MaxSizeMB { get; set; }

    /// <summary>
    /// Ruta de almacenamiento para archivos
    /// </summary>
    public string? StoragePath { get; set; }

    /// <summary>
    /// Prefijo para valores numéricos o monetarios
    /// </summary>
    public string? Prefix { get; set; }

    /// <summary>
    /// Valor mínimo para controles numéricos
    /// </summary>
    public object? Min { get; set; }

    /// <summary>
    /// Valor máximo para controles numéricos
    /// </summary>
    public object? Max { get; set; }

    /// <summary>
    /// Incremento/decremento para controles numéricos
    /// </summary>
    public object? Step { get; set; }

    /// <summary>
    /// Campos de los que depende este campo
    /// </summary>
    public string[]? DependsOn { get; set; }

    /// <summary>
    /// Condición para hacer el campo de solo lectura
    /// </summary>
    public string? ReadOnlyCondition { get; set; }

    /// <summary>
    /// Función para deshabilitar fechas en DatePicker
    /// </summary>
    public string? DisabledDate { get; set; }

    /// <summary>
    /// Formato de presentación de fechas o números
    /// </summary>
    public string? Format { get; set; }
}
