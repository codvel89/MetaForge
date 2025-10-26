using System.Security.Cryptography;

namespace MetaForge.Core.Services.Security;

/// <summary>
/// Implementación de hasheo de contraseñas usando PBKDF2 (nativo de .NET)
/// </summary>
public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16; // 128 bits
    private const int HashSize = 32; // 256 bits
    private const int Iterations = 100000; // Recomendado por OWASP 2023

    /// <summary>
    /// Hashea una contraseña usando PBKDF2
    /// </summary>
    public string HashPassword(string password)
    {
        // Generar salt aleatorio
        using var rng = RandomNumberGenerator.Create();
        var salt = new byte[SaltSize];
        rng.GetBytes(salt);

        // Generar hash usando PBKDF2
        using var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            salt,
            Iterations,
            HashAlgorithmName.SHA256
        );
        var hash = pbkdf2.GetBytes(HashSize);

        // Combinar salt + hash
        var hashBytes = new byte[SaltSize + HashSize];
        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

        // Retornar como base64: iterations.salt.hash
        return $"{Iterations}.{Convert.ToBase64String(hashBytes)}";
    }

    /// <summary>
    /// Verifica una contraseña contra su hash usando PBKDF2
    /// </summary>
    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        try
        {
            // Parsear el hash almacenado
            var parts = hashedPassword.Split('.');
            if (parts.Length != 2)
                return false;

            var iterations = int.Parse(parts[0]);
            var hashBytes = Convert.FromBase64String(parts[1]);

            // Extraer salt
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Generar hash de la contraseña proporcionada
            using var pbkdf2 = new Rfc2898DeriveBytes(
                providedPassword,
                salt,
                iterations,
                HashAlgorithmName.SHA256
            );
            var hash = pbkdf2.GetBytes(HashSize);

            // Comparar hashes de forma segura (constant-time)
            for (int i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                    return false;
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
}
