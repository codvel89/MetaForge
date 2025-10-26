namespace MetaForge.Core.Entities.Security;

/// <summary>
/// Representa un rol del sistema
/// </summary>
public class Role
{
    /// <summary>
    /// Identificador único del rol
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre del rol
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Nombre visible del rol
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del rol
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Indica si es un rol del sistema (no editable)
    /// </summary>
    public bool IsSystem { get; set; }

    /// <summary>
    /// Fecha de creación
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Usuarios con este rol
    /// </summary>
    public List<UserRole> UserRoles { get; set; } = new();

    /// <summary>
    /// Permisos del rol
    /// </summary>
    public List<RolePermission> RolePermissions { get; set; } = new();
}
