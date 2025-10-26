using Microsoft.EntityFrameworkCore;
using MetaForge.Shared;

namespace MetaForge.Core.Context;

/// <summary>
/// DbContext dinámico que construye su modelo a partir de definiciones de tabla
/// </summary>
public class DynamicDbContext : DbContext
{
    private readonly IEnumerable<TableDefinition> _tables;

    /// <summary>
    /// Constructor que acepta opciones y definiciones de tabla
    /// </summary>
    public DynamicDbContext(DbContextOptions<DynamicDbContext> options, IEnumerable<TableDefinition> tables)
        : base(options)
    {
        _tables = tables ?? throw new ArgumentNullException(nameof(tables));
    }

    /// <summary>
    /// Configura el modelo usando las definiciones de tabla proporcionadas
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Construir modelo dinámicamente
        DynamicModelBuilder.ConfigureModel(modelBuilder, _tables);
    }

    /// <summary>
    /// Obtiene un DbSet para una tabla específica por nombre
    /// </summary>
    public DbSet<Dictionary<string, object>> GetTable(string tableName)
    {
        return Set<Dictionary<string, object>>(tableName);
    }
}
