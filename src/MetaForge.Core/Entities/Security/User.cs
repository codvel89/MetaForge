namespace MetaForge.Core.Entities.Security;

/// <summary>
/// Representa un usuario del sistema
/// </summary>
public class User
{
    /// <summary>
    /// Identificador único del usuario
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre de usuario (login)
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Email del usuario
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Hash de la contraseña
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Nombre completo
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// Indica si el usuario está activo
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Indica si el email está verificado
    /// </summary>
    public bool EmailVerified { get; set; }

    /// <summary>
    /// Fecha de creación
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Fecha de último login
    /// </summary>
    public DateTime? LastLoginAt { get; set; }

    /// <summary>
    /// Token de refresh para JWT
    /// </summary>
    public string? RefreshToken { get; set; }

    /// <summary>
    /// Expiración del refresh token
    /// </summary>
    public DateTime? RefreshTokenExpiry { get; set; }

    /// <summary>
    /// Roles del usuario
    /// </summary>
    public List<UserRole> UserRoles { get; set; } = new();
}
