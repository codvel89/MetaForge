namespace MetaForge.Core.Entities.Security;

/// <summary>
/// Relación muchos a muchos entre roles y permisos
/// </summary>
public class RolePermission
{
    /// <summary>
    /// ID del rol
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    /// ID del permiso
    /// </summary>
    public int PermissionId { get; set; }

    /// <summary>
    /// Fecha de asignación
    /// </summary>
    public DateTime AssignedAt { get; set; }

    /// <summary>
    /// Rol
    /// </summary>
    public Role Role { get; set; } = null!;

    /// <summary>
    /// Permiso
    /// </summary>
    public Permission Permission { get; set; } = null!;
}
