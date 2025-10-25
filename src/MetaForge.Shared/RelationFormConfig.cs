namespace MetaForge.Shared;

/// <summary>
/// Configuración de presentación de relaciones en formularios
/// </summary>
public class RelationFormConfig
{
    /// <summary>
    /// Componente a utilizar (SubTable, Modal, Drawer, etc.)
    /// </summary>
    public string Component { get; set; } = string.Empty;

    /// <summary>
    /// Permite agregar nuevos registros
    /// </summary>
    public bool AllowAdd { get; set; }

    /// <summary>
    /// Permite editar registros existentes
    /// </summary>
    public bool AllowEdit { get; set; }

    /// <summary>
    /// Permite eliminar registros
    /// </summary>
    public bool AllowDelete { get; set; }

    /// <summary>
    /// Número mínimo de filas requeridas
    /// </summary>
    public int? MinRows { get; set; }

    /// <summary>
    /// Número máximo de filas permitidas
    /// </summary>
    public int? MaxRows { get; set; }

    /// <summary>
    /// Permite edición en línea en la tabla
    /// </summary>
    public bool InlineEditing { get; set; }
}
