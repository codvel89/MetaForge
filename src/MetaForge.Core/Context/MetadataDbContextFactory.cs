using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MetaForge.Core.Context;

/// <summary>
/// Factory para crear instancias de MetadataDbContext en tiempo de diseño (migraciones)
/// </summary>
public class MetadataDbContextFactory : IDesignTimeDbContextFactory<MetadataDbContext>
{
    /// <summary>
    /// Crea una instancia de MetadataDbContext para migraciones
    /// </summary>
    public MetadataDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MetadataDbContext>();
        
        // Usar PostgreSQL con una connection string por defecto para migraciones
        // Esta será sobrescrita en runtime con la connection string real
        optionsBuilder.UseNpgsql(
            "Host=localhost;Database=metaforge_system;Username=postgres;Password=postgres",
            options => options.MigrationsHistoryTable("__EFMigrationsHistory", "metaforge")
        );

        return new MetadataDbContext(optionsBuilder.Options);
    }
}
