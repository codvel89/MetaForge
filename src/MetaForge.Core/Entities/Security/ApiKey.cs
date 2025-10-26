namespace MetaForge.Core.Entities.Security;

/// <summary>
/// Representa una clave de API para integración
/// </summary>
public class ApiKey
{
    /// <summary>
    /// Identificador único
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre de la clave (identificador amigable)
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// La clave API (hash)
    /// </summary>
    public string KeyHash { get; set; } = string.Empty;

    /// <summary>
    /// Prefijo visible de la clave (ej: "pk_live_")
    /// </summary>
    public string KeyPrefix { get; set; } = string.Empty;

    /// <summary>
    /// ID del usuario dueño de la clave
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Indica si la clave está activa
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Fecha de creación
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Fecha de expiración (opcional)
    /// </summary>
    public DateTime? ExpiresAt { get; set; }

    /// <summary>
    /// Fecha de último uso
    /// </summary>
    public DateTime? LastUsedAt { get; set; }

    /// <summary>
    /// Permisos de la clave (JSON array de permisos)
    /// </summary>
    public string? Permissions { get; set; }
}
