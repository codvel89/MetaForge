namespace MetaForge.Shared;

/// <summary>
/// Representa una conexión a una base de datos
/// </summary>
public class DatabaseConnection
{
    /// <summary>
    /// Identificador único de la conexión
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre descriptivo de la conexión
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Proveedor de base de datos
    /// </summary>
    public DatabaseProvider Provider { get; set; }

    /// <summary>
    /// Cadena de conexión
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// Esquema predeterminado
    /// </summary>
    public string? DefaultSchema { get; set; }

    /// <summary>
    /// Indica si la conexión está activa
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Timeout de conexión en segundos
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// Tamaño del pool de conexiones
    /// </summary>
    public int PoolSize { get; set; } = 100;

    /// <summary>
    /// Indica si se debe usar SSL/TLS
    /// </summary>
    public bool UseSsl { get; set; }

    /// <summary>
    /// Descripción de la conexión
    /// </summary>
    public string? Description { get; set; }
}
