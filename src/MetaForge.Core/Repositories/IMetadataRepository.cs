using MetaForge.Shared;

namespace MetaForge.Core.Repositories;

/// <summary>
/// Repositorio para operaciones con metadatos del sistema
/// </summary>
public interface IMetadataRepository
{
    // ========== TableDefinitions ==========
    
    /// <summary>
    /// Obtiene todas las definiciones de tabla
    /// </summary>
    Task<List<TableDefinition>> GetAllTablesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene una definición de tabla por ID
    /// </summary>
    Task<TableDefinition?> GetTableByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene una definición de tabla por nombre
    /// </summary>
    Task<TableDefinition?> GetTableByNameAsync(string name, string schema = "public", CancellationToken cancellationToken = default);

    /// <summary>
    /// Crea o actualiza una definición de tabla
    /// </summary>
    Task<TableDefinition> SaveTableAsync(TableDefinition table, CancellationToken cancellationToken = default);

    /// <summary>
    /// Elimina una definición de tabla
    /// </summary>
    Task DeleteTableAsync(int id, CancellationToken cancellationToken = default);

    // ========== DatabaseConnections ==========

    /// <summary>
    /// Obtiene todas las conexiones de base de datos
    /// </summary>
    Task<List<DatabaseConnection>> GetAllConnectionsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene las conexiones activas
    /// </summary>
    Task<List<DatabaseConnection>> GetActiveConnectionsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene una conexión por ID
    /// </summary>
    Task<DatabaseConnection?> GetConnectionByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene una conexión por nombre
    /// </summary>
    Task<DatabaseConnection?> GetConnectionByNameAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Crea o actualiza una conexión
    /// </summary>
    Task<DatabaseConnection> SaveConnectionAsync(DatabaseConnection connection, CancellationToken cancellationToken = default);

    /// <summary>
    /// Elimina una conexión
    /// </summary>
    Task DeleteConnectionAsync(int id, CancellationToken cancellationToken = default);
}
