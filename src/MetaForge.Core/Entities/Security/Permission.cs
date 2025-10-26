namespace MetaForge.Core.Entities.Security;

/// <summary>
/// Representa un permiso granular del sistema
/// </summary>
public class Permission
{
    /// <summary>
    /// Identificador único del permiso
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre del permiso (ej: "users.create", "tables.delete")
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Recurso al que aplica el permiso (ej: "users", "tables")
    /// </summary>
    public string Resource { get; set; } = string.Empty;

    /// <summary>
    /// Acción del permiso (ej: "create", "read", "update", "delete")
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Nombre visible del permiso
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del permiso
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Categoría del permiso (ej: "Users", "Tables", "Modules")
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Roles que tienen este permiso
    /// </summary>
    public List<RolePermission> RolePermissions { get; set; } = new();
}
