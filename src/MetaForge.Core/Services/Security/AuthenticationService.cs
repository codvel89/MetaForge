using MetaForge.Core.Entities.Security;
using MetaForge.Core.Repositories;

namespace MetaForge.Core.Services.Security;

/// <summary>
/// Implementación del servicio de autenticación
/// </summary>
public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IAuthorizationService _authorizationService;

    public AuthenticationService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService,
        IAuthorizationService authorizationService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
        _authorizationService = authorizationService;
    }

    /// <summary>
    /// Autentica un usuario con credenciales
    /// </summary>
    public async Task<AuthenticationResult> AuthenticateAsync(string username, string password)
    {
        // Buscar usuario por username o email
        var user = await _userRepository.GetByUsernameOrEmailAsync(username);

        if (user == null)
        {
            return new AuthenticationResult
            {
                Success = false,
                ErrorMessage = "Invalid username or password"
            };
        }

        // Verificar si el usuario está activo
        if (!user.IsActive)
        {
            return new AuthenticationResult
            {
                Success = false,
                ErrorMessage = "User account is inactive"
            };
        }

        // Verificar contraseña
        if (!_passwordHasher.VerifyPassword(user.PasswordHash, password))
        {
            return new AuthenticationResult
            {
                Success = false,
                ErrorMessage = "Invalid username or password"
            };
        }

        // Obtener roles y permisos
        var roles = await _authorizationService.GetUserRolesAsync(user.Id);
        var permissions = await _authorizationService.GetUserPermissionsAsync(user.Id);

        // Generar tokens
        var accessToken = _jwtTokenService.GenerateAccessToken(user, roles, permissions);
        var refreshToken = _jwtTokenService.GenerateRefreshToken();
        var refreshTokenExpiry = DateTime.UtcNow.AddDays(7); // 7 días

        // Actualizar refresh token en BD
        await _userRepository.UpdateRefreshTokenAsync(user.Id, refreshToken, refreshTokenExpiry);

        // Actualizar último login
        await _userRepository.UpdateLastLoginAsync(user.Id);

        return new AuthenticationResult
        {
            Success = true,
            User = user,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60) // Debe coincidir con JWT expiration
        };
    }

    /// <summary>
    /// Refresca un token de acceso usando un refresh token
    /// </summary>
    public async Task<AuthenticationResult> RefreshTokenAsync(string refreshToken)
    {
        // Buscar usuario por refresh token (necesitamos agregar este método)
        var users = await _userRepository.GetAllAsync(includeInactive: false);
        var user = users.FirstOrDefault(u => u.RefreshToken == refreshToken);

        if (user == null)
        {
            return new AuthenticationResult
            {
                Success = false,
                ErrorMessage = "Invalid refresh token"
            };
        }

        // Verificar si el refresh token ha expirado
        if (user.RefreshTokenExpiry == null || user.RefreshTokenExpiry < DateTime.UtcNow)
        {
            return new AuthenticationResult
            {
                Success = false,
                ErrorMessage = "Refresh token has expired"
            };
        }

        // Obtener roles y permisos
        var roles = await _authorizationService.GetUserRolesAsync(user.Id);
        var permissions = await _authorizationService.GetUserPermissionsAsync(user.Id);

        // Generar nuevos tokens
        var newAccessToken = _jwtTokenService.GenerateAccessToken(user, roles, permissions);
        var newRefreshToken = _jwtTokenService.GenerateRefreshToken();
        var newRefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

        // Actualizar refresh token en BD
        await _userRepository.UpdateRefreshTokenAsync(user.Id, newRefreshToken, newRefreshTokenExpiry);

        return new AuthenticationResult
        {
            Success = true,
            User = user,
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60)
        };
    }

    /// <summary>
    /// Revoca un refresh token
    /// </summary>
    public async Task RevokeTokenAsync(string refreshToken)
    {
        var users = await _userRepository.GetAllAsync(includeInactive: false);
        var user = users.FirstOrDefault(u => u.RefreshToken == refreshToken);

        if (user != null)
        {
            // Limpiar el refresh token
            await _userRepository.UpdateRefreshTokenAsync(user.Id, string.Empty, DateTime.UtcNow);
        }
    }

    /// <summary>
    /// Cambia la contraseña de un usuario
    /// </summary>
    public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
            return false;

        // Verificar contraseña actual
        if (!_passwordHasher.VerifyPassword(user.PasswordHash, currentPassword))
            return false;

        // Hashear nueva contraseña
        user.PasswordHash = _passwordHasher.HashPassword(newPassword);

        // Guardar cambios
        await _userRepository.UpdateAsync(user);

        // Revocar todos los tokens existentes por seguridad
        if (!string.IsNullOrEmpty(user.RefreshToken))
        {
            await _userRepository.UpdateRefreshTokenAsync(user.Id, string.Empty, DateTime.UtcNow);
        }

        return true;
    }
}
