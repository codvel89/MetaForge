namespace MetaForge.Core.Services.Security;

/// <summary>
/// Servicio para hashear y verificar contraseñas
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hashea una contraseña
    /// </summary>
    /// <param name="password">Contraseña en texto plano</param>
    /// <returns>Hash de la contraseña</returns>
    string HashPassword(string password);

    /// <summary>
    /// Verifica una contraseña contra su hash
    /// </summary>
    /// <param name="hashedPassword">Hash almacenado</param>
    /// <param name="providedPassword">Contraseña proporcionada</param>
    /// <returns>True si coincide, False si no</returns>
    bool VerifyPassword(string hashedPassword, string providedPassword);
}
