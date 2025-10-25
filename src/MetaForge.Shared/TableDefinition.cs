namespace MetaForge.Shared;

/// <summary>
/// Define una tabla completa con todos sus metadatos
/// </summary>
public class TableDefinition
{
    /// <summary>
    /// Identificador único de la tabla
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre de la tabla en la base de datos
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Esquema de la base de datos
    /// </summary>
    public string Schema { get; set; } = "public";

    /// <summary>
    /// Nombre visible de la tabla
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Descripción de la tabla
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Icono asociado a la tabla
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// Nombre en plural
    /// </summary>
    public string? PluralName { get; set; }

    /// <summary>
    /// Nombre en singular
    /// </summary>
    public string? SingularName { get; set; }

    /// <summary>
    /// Indica si es una tabla del sistema
    /// </summary>
    public bool IsSystemTable { get; set; }

    /// <summary>
    /// Habilita auditoría de cambios
    /// </summary>
    public bool EnableAudit { get; set; }

    /// <summary>
    /// Habilita eliminación lógica (soft delete)
    /// </summary>
    public bool EnableSoftDelete { get; set; }

    /// <summary>
    /// Habilita workflow para esta tabla
    /// </summary>
    public bool EnableWorkflow { get; set; }

    /// <summary>
    /// Identificador de definición de workflow
    /// </summary>
    public string? WorkflowDefinitionId { get; set; }

    /// <summary>
    /// Columnas de la tabla
    /// </summary>
    public List<ColumnDefinition> Columns { get; set; } = new();

    /// <summary>
    /// Índices de la tabla
    /// </summary>
    public List<IndexDefinition>? Indexes { get; set; }

    /// <summary>
    /// Relaciones con otras tablas
    /// </summary>
    public List<RelationDefinition>? Relations { get; set; }

    /// <summary>
    /// Campos calculados
    /// </summary>
    public List<ComputedFieldDefinition>? ComputedFields { get; set; }

    /// <summary>
    /// Triggers definidos
    /// </summary>
    public List<TriggerDefinition>? Triggers { get; set; }

    /// <summary>
    /// Reglas de negocio
    /// </summary>
    public List<BusinessRule>? BusinessRules { get; set; }

    /// <summary>
    /// Permisos de acceso
    /// </summary>
    public TablePermissions? Permissions { get; set; }

    /// <summary>
    /// Configuración de vista de lista
    /// </summary>
    public ListViewConfig? ListViewConfig { get; set; }

    /// <summary>
    /// Configuración de vista de formulario
    /// </summary>
    public FormViewConfig? FormViewConfig { get; set; }
}
