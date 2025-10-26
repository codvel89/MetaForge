using Microsoft.EntityFrameworkCore;
using MetaForge.Shared;
using MetaForge.Core.Context;

namespace MetaForge.Core.Repositories;

/// <summary>
/// Implementación del repositorio de metadatos
/// </summary>
public class MetadataRepository : IMetadataRepository
{
    private readonly MetadataDbContext _context;

    /// <summary>
    /// Constructor con inyección de dependencias
    /// </summary>
    public MetadataRepository(MetadataDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // ========== TableDefinitions ==========

    /// <summary>
    /// Obtiene todas las definiciones de tabla con sus relaciones
    /// </summary>
    public async Task<List<TableDefinition>> GetAllTablesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.TableDefinitions
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Obtiene una definición de tabla por ID
    /// </summary>
    public async Task<TableDefinition?> GetTableByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.TableDefinitions
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    /// <summary>
    /// Obtiene una definición de tabla por nombre y esquema
    /// </summary>
    public async Task<TableDefinition?> GetTableByNameAsync(string name, string schema = "public", CancellationToken cancellationToken = default)
    {
        return await _context.TableDefinitions
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Name == name && t.Schema == schema, cancellationToken);
    }

    /// <summary>
    /// Crea o actualiza una definición de tabla
    /// </summary>
    public async Task<TableDefinition> SaveTableAsync(TableDefinition table, CancellationToken cancellationToken = default)
    {
        if (table.Id == 0)
        {
            // Nueva tabla
            _context.TableDefinitions.Add(table);
        }
        else
        {
            // Actualizar existente
            var existing = await _context.TableDefinitions.FindAsync(new object[] { table.Id }, cancellationToken);
            if (existing == null)
            {
                throw new InvalidOperationException($"Table with ID {table.Id} not found");
            }

            _context.Entry(existing).CurrentValues.SetValues(table);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return table;
    }

    /// <summary>
    /// Elimina una definición de tabla
    /// </summary>
    public async Task DeleteTableAsync(int id, CancellationToken cancellationToken = default)
    {
        var table = await _context.TableDefinitions.FindAsync(new object[] { id }, cancellationToken);
        if (table != null)
        {
            _context.TableDefinitions.Remove(table);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    // ========== DatabaseConnections ==========

    /// <summary>
    /// Obtiene todas las conexiones de base de datos
    /// </summary>
    public async Task<List<DatabaseConnection>> GetAllConnectionsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.DatabaseConnections
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Obtiene las conexiones activas
    /// </summary>
    public async Task<List<DatabaseConnection>> GetActiveConnectionsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.DatabaseConnections
            .AsNoTracking()
            .Where(c => c.IsActive)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Obtiene una conexión por ID
    /// </summary>
    public async Task<DatabaseConnection?> GetConnectionByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.DatabaseConnections
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    /// <summary>
    /// Obtiene una conexión por nombre
    /// </summary>
    public async Task<DatabaseConnection?> GetConnectionByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.DatabaseConnections
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Name == name, cancellationToken);
    }

    /// <summary>
    /// Crea o actualiza una conexión
    /// </summary>
    public async Task<DatabaseConnection> SaveConnectionAsync(DatabaseConnection connection, CancellationToken cancellationToken = default)
    {
        if (connection.Id == 0)
        {
            // Nueva conexión
            _context.DatabaseConnections.Add(connection);
        }
        else
        {
            // Actualizar existente
            var existing = await _context.DatabaseConnections.FindAsync(new object[] { connection.Id }, cancellationToken);
            if (existing == null)
            {
                throw new InvalidOperationException($"Connection with ID {connection.Id} not found");
            }

            _context.Entry(existing).CurrentValues.SetValues(connection);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return connection;
    }

    /// <summary>
    /// Elimina una conexión
    /// </summary>
    public async Task DeleteConnectionAsync(int id, CancellationToken cancellationToken = default)
    {
        var connection = await _context.DatabaseConnections.FindAsync(new object[] { id }, cancellationToken);
        if (connection != null)
        {
            _context.DatabaseConnections.Remove(connection);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
