using Microsoft.EntityFrameworkCore;
using MetaForge.Shared;
using MetaForge.Core.Context;

namespace MetaForge.Core.Factories;

/// <summary>
/// Fábrica para crear instancias de DynamicDbContext con el proveedor correcto
/// </summary>
public static class DbContextFactory
{
    /// <summary>
    /// Crea un DynamicDbContext configurado para el proveedor de base de datos especificado
    /// </summary>
    /// <param name="connection">Configuración de conexión a la base de datos</param>
    /// <param name="tables">Definiciones de tablas para el modelo</param>
    /// <returns>Instancia configurada de DynamicDbContext</returns>
    public static DynamicDbContext Create(DatabaseConnection connection, IEnumerable<TableDefinition> tables)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DynamicDbContext>();

        ConfigureProvider(optionsBuilder, connection);

        return new DynamicDbContext(optionsBuilder.Options, tables);
    }

    /// <summary>
    /// Configura PostgreSQL como proveedor de base de datos
    /// </summary>
    private static void ConfigureProvider(DbContextOptionsBuilder<DynamicDbContext> optionsBuilder, DatabaseConnection connection)
    {
        optionsBuilder.UseNpgsql(
            connection.ConnectionString,
            options => options.CommandTimeout(connection.TimeoutSeconds)
        );

        // Configurar opciones comunes
        optionsBuilder.EnableSensitiveDataLogging(false);
        optionsBuilder.EnableDetailedErrors(true);
    }
}
