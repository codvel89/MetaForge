using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MetaForge.Core.Entities.Security;
using Microsoft.IdentityModel.Tokens;

namespace MetaForge.Core.Services.Security;

/// <summary>
/// Implementación del servicio de tokens JWT
/// </summary>
public class JwtTokenService : IJwtTokenService
{
    private readonly ISettingsService _settingsService;
    private readonly string _secretKey;

    /// <summary>
    /// Constructor
    /// </summary>
    public JwtTokenService(ISettingsService settingsService)
    {
        _settingsService = settingsService;
        
        // JWT_SECRET_KEY debe venir de variable de entorno (sensible)
        _secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") 
            ?? throw new InvalidOperationException("JWT_SECRET_KEY environment variable not configured");
        
        if (_secretKey.Length < 32)
            throw new InvalidOperationException("JWT_SECRET_KEY must be at least 32 characters long");
    }

    /// <summary>
    /// Genera un token de acceso JWT
    /// </summary>
    public string GenerateAccessToken(User user, IEnumerable<string> roles, IEnumerable<string> permissions)
    {
        // Obtener configuración desde SystemSettings
        var issuer = _settingsService.GetSettingAsync("jwt.issuer", "MetaForge").Result;
        var audience = _settingsService.GetSettingAsync("jwt.audience", "MetaForge.API").Result;
        var expirationMinutes = _settingsService.GetSettingAsync<int>("jwt.expiration_minutes", 60).Result;

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Email, user.Email),
            new("user_id", user.Id.ToString())
        };

        // Agregar roles como claims
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        // Agregar permisos como claims
        foreach (var permission in permissions)
        {
            claims.Add(new Claim("permission", permission));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// Genera un refresh token aleatorio
    /// </summary>
    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    /// <summary>
    /// Valida un token JWT
    /// </summary>
    public int? ValidateToken(string token)
    {
        try
        {
            var issuer = _settingsService.GetSettingAsync("jwt.issuer", "MetaForge").Result;
            var audience = _settingsService.GetSettingAsync("jwt.audience", "MetaForge.API").Result;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userIdClaim = jwtToken.Claims.First(x => x.Type == "user_id");
            return int.Parse(userIdClaim.Value);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Obtiene el ID de usuario desde un token
    /// </summary>
    public int GetUserIdFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        var userIdClaim = jwtToken.Claims.First(x => x.Type == "user_id");
        return int.Parse(userIdClaim.Value);
    }
}
