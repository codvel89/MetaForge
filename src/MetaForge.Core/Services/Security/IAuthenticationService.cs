using MetaForge.Core.Entities.Security;

namespace MetaForge.Core.Services.Security;

/// <summary>
/// Resultado de autenticación
/// </summary>
public class AuthenticationResult
{
    /// <summary>
    /// Indica si la autenticación fue exitosa
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Mensaje de error si falló
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Usuario autenticado
    /// </summary>
    public User? User { get; set; }

    /// <summary>
    /// Token de acceso JWT
    /// </summary>
    public string? AccessToken { get; set; }

    /// <summary>
    /// Refresh token
    /// </summary>
    public string? RefreshToken { get; set; }

    /// <summary>
    /// Fecha de expiración del token
    /// </summary>
    public DateTime? ExpiresAt { get; set; }
}

/// <summary>
/// Servicio de autenticación de usuarios
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Autentica un usuario con credenciales
    /// </summary>
    /// <param name="username">Nombre de usuario o email</param>
    /// <param name="password">Contraseña</param>
    /// <returns>Resultado de autenticación</returns>
    Task<AuthenticationResult> AuthenticateAsync(string username, string password);

    /// <summary>
    /// Refresca un token de acceso usando un refresh token
    /// </summary>
    /// <param name="refreshToken">Refresh token</param>
    /// <returns>Resultado de autenticación</returns>
    Task<AuthenticationResult> RefreshTokenAsync(string refreshToken);

    /// <summary>
    /// Revoca un refresh token
    /// </summary>
    /// <param name="refreshToken">Refresh token a revocar</param>
    Task RevokeTokenAsync(string refreshToken);

    /// <summary>
    /// Cambia la contraseña de un usuario
    /// </summary>
    /// <param name="userId">ID del usuario</param>
    /// <param name="currentPassword">Contraseña actual</param>
    /// <param name="newPassword">Nueva contraseña</param>
    /// <returns>True si se cambió exitosamente</returns>
    Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
}
