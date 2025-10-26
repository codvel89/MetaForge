namespace MetaForge.Core.Entities.Security;

/// <summary>
/// Relación muchos a muchos entre usuarios y roles
/// </summary>
public class UserRole
{
    /// <summary>
    /// Identificador único
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID del usuario
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// ID del rol
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    /// Fecha de asignación
    /// </summary>
    public DateTime AssignedAt { get; set; }

    /// <summary>
    /// Usuario
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Rol
    /// </summary>
    public Role Role { get; set; } = null!;
}
