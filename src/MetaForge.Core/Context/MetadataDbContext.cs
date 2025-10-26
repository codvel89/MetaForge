using Microsoft.EntityFrameworkCore;
using MetaForge.Shared;

namespace MetaForge.Core.Context;

/// <summary>
/// DbContext para almacenar los metadatos del sistema (TableDefinitions, ColumnDefinitions, etc.)
/// </summary>
public class MetadataDbContext : DbContext
{
    /// <summary>
    /// Constructor con opciones de configuración
    /// </summary>
    public MetadataDbContext(DbContextOptions<MetadataDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Definiciones de tablas
    /// </summary>
    public DbSet<TableDefinition> TableDefinitions { get; set; } = null!;

    /// <summary>
    /// Conexiones a bases de datos de aplicación
    /// </summary>
    public DbSet<DatabaseConnection> DatabaseConnections { get; set; } = null!;

    /// <summary>
    /// Configuración del modelo
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureTableDefinition(modelBuilder);
        ConfigureDatabaseConnection(modelBuilder);
    }

    /// <summary>
    /// Configura la entidad TableDefinition
    /// </summary>
    private void ConfigureTableDefinition(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TableDefinition>(entity =>
        {
            entity.ToTable("TableDefinitions", "metaforge");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Schema).IsRequired().HasMaxLength(50);
            entity.Property(e => e.DisplayName).HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(500);

            // Índice único por nombre de tabla y esquema
            entity.HasIndex(e => new { e.Name, e.Schema }).IsUnique();

            // Relación con columnas - owned entity
            entity.OwnsMany(e => e.Columns, columns =>
            {
                columns.ToTable("ColumnDefinitions", "metaforge");
                columns.WithOwner().HasForeignKey("TableDefinitionId");
                columns.Property<int>("Id").ValueGeneratedOnAdd();
                columns.HasKey("Id");

                columns.Property(c => c.Name).IsRequired().HasMaxLength(100);
                columns.Property(c => c.Type).IsRequired().HasMaxLength(50);
                columns.Property(c => c.DisplayName).HasMaxLength(200);

                // Owned collections dentro de columnas
                columns.OwnsMany(c => c.ValidationRules, rules =>
                {
                    rules.ToTable("ValidationRules", "metaforge");
                    rules.WithOwner();
                    rules.Property<int>("Id").ValueGeneratedOnAdd();
                    rules.HasKey("Id");
                });

                columns.OwnsOne(c => c.FormConfig, config =>
                {
                    config.ToTable("FieldFormConfigs", "metaforge");
                    config.WithOwner();
                    
                    config.OwnsMany(f => f.Options, opts =>
                    {
                        opts.ToTable("SelectOptions", "metaforge");
                        opts.WithOwner();
                        opts.Property<int>("Id").ValueGeneratedOnAdd();
                        opts.HasKey("Id");
                    });

                    config.OwnsOne(f => f.DataSource);
                });

                columns.OwnsOne(c => c.SequenceConfig);
                columns.OwnsMany(c => c.StateTransitions);
            });

            // Owned collections de la tabla
            entity.OwnsMany(e => e.Indexes, indexes =>
            {
                indexes.ToTable("IndexDefinitions", "metaforge");
                indexes.WithOwner().HasForeignKey("TableDefinitionId");
                indexes.Property<int>("Id").ValueGeneratedOnAdd();
                indexes.HasKey("Id");
            });

            entity.OwnsMany(e => e.Relations, relations =>
            {
                relations.ToTable("RelationDefinitions", "metaforge");
                relations.WithOwner().HasForeignKey("TableDefinitionId");
                relations.Property<int>("Id").ValueGeneratedOnAdd();
                relations.HasKey("Id");

                relations.OwnsOne(r => r.FormConfig);
            });

            entity.OwnsMany(e => e.ComputedFields, computed =>
            {
                computed.ToTable("ComputedFieldDefinitions", "metaforge");
                computed.WithOwner().HasForeignKey("TableDefinitionId");
                computed.Property<int>("Id").ValueGeneratedOnAdd();
                computed.HasKey("Id");
            });

            entity.OwnsMany(e => e.Triggers, triggers =>
            {
                triggers.ToTable("TriggerDefinitions", "metaforge");
                triggers.WithOwner().HasForeignKey("TableDefinitionId");
                triggers.Property<int>("Id").ValueGeneratedOnAdd();
                triggers.HasKey("Id");

                triggers.OwnsMany(t => t.Actions);
            });

            entity.OwnsMany(e => e.BusinessRules, rules =>
            {
                rules.ToTable("BusinessRules", "metaforge");
                rules.WithOwner().HasForeignKey("TableDefinitionId");
                rules.Property<int>("Id").ValueGeneratedOnAdd();
                rules.HasKey("Id");
            });

            entity.OwnsOne(e => e.Permissions);
            entity.OwnsOne(e => e.ListViewConfig, config =>
            {
                config.OwnsMany(l => l.QuickFilters);
                config.OwnsMany(l => l.Aggregations);
            });
            entity.OwnsOne(e => e.FormViewConfig, config =>
            {
                config.OwnsMany(f => f.Sections);
            });
        });
    }

    /// <summary>
    /// Configura la entidad DatabaseConnection
    /// </summary>
    private void ConfigureDatabaseConnection(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DatabaseConnection>(entity =>
        {
            entity.ToTable("DatabaseConnections", "metaforge");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.ConnectionString).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.DefaultSchema).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(500);

            entity.HasIndex(e => e.Name).IsUnique();
        });
    }
}
