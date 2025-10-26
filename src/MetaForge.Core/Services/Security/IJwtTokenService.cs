using MetaForge.Core.Entities.Security;

namespace MetaForge.Core.Services.Security;

/// <summary>
/// Servicio para generación y validación de tokens JWT
/// </summary>
public interface IJwtTokenService
{
    /// <summary>
    /// Genera un token de acceso JWT para un usuario
    /// </summary>
    /// <param name="user">Usuario</param>
    /// <param name="roles">Roles del usuario</param>
    /// <param name="permissions">Permisos del usuario</param>
    /// <returns>Token JWT</returns>
    string GenerateAccessToken(User user, IEnumerable<string> roles, IEnumerable<string> permissions);

    /// <summary>
    /// Genera un refresh token
    /// </summary>
    /// <returns>Refresh token</returns>
    string GenerateRefreshToken();

    /// <summary>
    /// Valida un token JWT
    /// </summary>
    /// <param name="token">Token a validar</param>
    /// <returns>ID del usuario si es válido, null si no</returns>
    int? ValidateToken(string token);

    /// <summary>
    /// Obtiene el ID de usuario desde un token
    /// </summary>
    /// <param name="token">Token JWT</param>
    /// <returns>ID del usuario</returns>
    int GetUserIdFromToken(string token);
}
