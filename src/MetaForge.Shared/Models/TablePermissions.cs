namespace MetaForge.Shared.Models;

/// <summary>
/// Define los permisos de acceso a una tabla
/// </summary>
public class TablePermissions
{
    /// <summary>
    /// Roles que pueden crear registros
    /// </summary>
    public string[] Create { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Roles que pueden leer registros
    /// </summary>
    public string[] Read { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Roles que pueden actualizar registros
    /// </summary>
    public string[] Update { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Roles que pueden eliminar registros
    /// </summary>
    public string[] Delete { get; set; } = Array.Empty<string>();
}
