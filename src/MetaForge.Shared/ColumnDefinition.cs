namespace MetaForge.Shared;

/// <summary>
/// Define una columna de una tabla
/// </summary>
public class ColumnDefinition
{
    /// <summary>
    /// Identificador único de la columna
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre de la columna en la base de datos
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Nombre visible de la columna
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Tipo de dato de la columna
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Longitud máxima para campos de texto
    /// </summary>
    public int? MaxLength { get; set; }

    /// <summary>
    /// Precisión para campos numéricos decimales
    /// </summary>
    public int? Precision { get; set; }

    /// <summary>
    /// Escala para campos numéricos decimales
    /// </summary>
    public int? Scale { get; set; }

    /// <summary>
    /// Indica si el campo es requerido
    /// </summary>
    public bool IsRequired { get; set; }

    /// <summary>
    /// Indica si es clave primaria
    /// </summary>
    public bool IsPrimaryKey { get; set; }

    /// <summary>
    /// Indica si es autoincremental
    /// </summary>
    public bool IsAutoIncrement { get; set; }

    /// <summary>
    /// Indica si es clave foránea
    /// </summary>
    public bool IsForeignKey { get; set; }

    /// <summary>
    /// Indica si el valor debe ser único
    /// </summary>
    public bool IsUnique { get; set; }

    /// <summary>
    /// Indica si tiene índice
    /// </summary>
    public bool IsIndexed { get; set; }

    /// <summary>
    /// Indica si es de solo lectura
    /// </summary>
    public bool IsReadOnly { get; set; }

    /// <summary>
    /// Valor predeterminado
    /// </summary>
    public string? DefaultValue { get; set; }

    /// <summary>
    /// Indica si se muestra en listas
    /// </summary>
    public bool ShowInList { get; set; }

    /// <summary>
    /// Indica si se muestra en formularios
    /// </summary>
    public bool ShowInForm { get; set; }

    /// <summary>
    /// Orden de presentación
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// Reglas de validación del campo
    /// </summary>
    public List<ValidationRule>? ValidationRules { get; set; }

    /// <summary>
    /// Configuración de presentación en formularios
    /// </summary>
    public FieldFormConfig? FormConfig { get; set; }

    /// <summary>
    /// Configuración de secuencia automática
    /// </summary>
    public SequenceConfig? SequenceConfig { get; set; }

    /// <summary>
    /// Transiciones de estado permitidas (para campos de estado)
    /// </summary>
    public List<StateTransition>? StateTransitions { get; set; }

    /// <summary>
    /// Descripción del campo
    /// </summary>
    public string? Description { get; set; }
}
