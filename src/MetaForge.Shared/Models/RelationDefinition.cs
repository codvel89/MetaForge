namespace MetaForge.Shared.Models;

/// <summary>
/// Define una relación entre tablas
/// </summary>
public class RelationDefinition
{
    /// <summary>
    /// Nombre de la relación
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de relación
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Columna de clave foránea en la tabla actual
    /// </summary>
    public string? ForeignKeyColumn { get; set; }

    /// <summary>
    /// Tabla relacionada
    /// </summary>
    public string RelatedTable { get; set; } = string.Empty;

    /// <summary>
    /// Columna en la tabla relacionada
    /// </summary>
    public string? RelatedColumn { get; set; }

    /// <summary>
    /// Clave foránea en la tabla relacionada (para relaciones OneToMany)
    /// </summary>
    public string? RelatedForeignKey { get; set; }

    /// <summary>
    /// Acción al eliminar (Cascade, Restrict, SetNull, NoAction)
    /// </summary>
    public string OnDelete { get; set; } = "Restrict";

    /// <summary>
    /// Indica si se muestra en el formulario
    /// </summary>
    public bool DisplayInForm { get; set; }

    /// <summary>
    /// Indica si se carga por defecto
    /// </summary>
    public bool LoadByDefault { get; set; }

    /// <summary>
    /// Indica si se eliminan en cascada los registros relacionados
    /// </summary>
    public bool CascadeDelete { get; set; }

    /// <summary>
    /// Configuración de presentación en formularios
    /// </summary>
    public RelationFormConfig? FormConfig { get; set; }
}
