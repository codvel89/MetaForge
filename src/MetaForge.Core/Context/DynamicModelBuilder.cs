using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MetaForge.Shared;

namespace MetaForge.Core.Context;

/// <summary>
/// Construye modelos de Entity Framework Core dinámicamente desde TableDefinition
/// </summary>
public static class DynamicModelBuilder
{
    /// <summary>
    /// Configura el modelo EF Core a partir de una lista de definiciones de tabla
    /// </summary>
    public static void ConfigureModel(ModelBuilder modelBuilder, IEnumerable<TableDefinition> tables)
    {
        foreach (var table in tables)
        {
            ConfigureTable(modelBuilder, table);
        }
    }

    /// <summary>
    /// Configura una tabla individual
    /// </summary>
    private static void ConfigureTable(ModelBuilder modelBuilder, TableDefinition table)
    {
        // Crear entidad dinámica como Dictionary<string, object>
        var entityType = modelBuilder.SharedTypeEntity<Dictionary<string, object>>(table.Name);

        // Configurar tabla y esquema
        entityType.ToTable(table.Name, table.Schema);

        // Configurar columnas
        foreach (var column in table.Columns.OrderBy(c => c.Order))
        {
            ConfigureColumn(entityType, column);
        }

        // Configurar clave primaria
        var primaryKeys = table.Columns.Where(c => c.IsPrimaryKey).Select(c => c.Name).ToArray();
        if (primaryKeys.Any())
        {
            entityType.HasKey(primaryKeys);
        }

        // Configurar índices
        if (table.Indexes != null)
        {
            foreach (var index in table.Indexes)
            {
                ConfigureIndex(entityType, index);
            }
        }

        // Configurar relaciones
        if (table.Relations != null)
        {
            foreach (var relation in table.Relations)
            {
                ConfigureRelation(modelBuilder, entityType, table, relation);
            }
        }
    }

    /// <summary>
    /// Configura una columna individual
    /// </summary>
    private static void ConfigureColumn(EntityTypeBuilder<Dictionary<string, object>> entityType, ColumnDefinition column)
    {
        var propertyBuilder = entityType.IndexerProperty(MapToClrType(column.Type), column.Name);

        // Configurar si es requerido
        if (column.IsRequired)
        {
            propertyBuilder.IsRequired();
        }

        // Configurar longitud máxima
        if (column.MaxLength.HasValue)
        {
            propertyBuilder.HasMaxLength(column.MaxLength.Value);
        }

        // Configurar precisión para decimales
        if (column.Type.ToLowerInvariant() == "decimal" && column.Precision.HasValue)
        {
            propertyBuilder.HasPrecision(column.Precision.Value, column.Scale ?? 2);
        }

        // Configurar auto-incremento
        if (column.IsAutoIncrement)
        {
            propertyBuilder.ValueGeneratedOnAdd();
        }

        // Configurar valor predeterminado
        if (!string.IsNullOrEmpty(column.DefaultValue) && column.DefaultValue != "SEQUENCE" && column.DefaultValue != "CURRENT_TIMESTAMP")
        {
            propertyBuilder.HasDefaultValueSql(column.DefaultValue);
        }
        else if (column.DefaultValue == "CURRENT_TIMESTAMP")
        {
            propertyBuilder.HasDefaultValueSql("CURRENT_TIMESTAMP");
        }

        // Configurar nombre de columna si es diferente
        propertyBuilder.HasColumnName(column.Name);
    }

    /// <summary>
    /// Configura un índice
    /// </summary>
    private static void ConfigureIndex(EntityTypeBuilder<Dictionary<string, object>> entityType, IndexDefinition index)
    {
        var indexBuilder = entityType.HasIndex(index.Columns);
        
        if (index.IsUnique)
        {
            indexBuilder.IsUnique();
        }

        indexBuilder.HasDatabaseName(index.Name);
    }

    /// <summary>
    /// Configura una relación
    /// </summary>
    private static void ConfigureRelation(
        ModelBuilder modelBuilder,
        EntityTypeBuilder<Dictionary<string, object>> entityType,
        TableDefinition table,
        RelationDefinition relation)
    {
        // Las relaciones se configurarán en una fase posterior
        // Por ahora solo configuramos las foreign keys
        if (relation.Type == "ManyToOne" && !string.IsNullOrEmpty(relation.ForeignKeyColumn))
        {
            entityType.HasOne(relation.RelatedTable, relation.Name)
                .WithMany()
                .HasForeignKey(relation.ForeignKeyColumn)
                .HasPrincipalKey(relation.RelatedColumn ?? "Id")
                .OnDelete(MapDeleteBehavior(relation.OnDelete));
        }
    }

    /// <summary>
    /// Mapea el tipo de dato de MetaForge al tipo CLR
    /// </summary>
    private static Type MapToClrType(string metaForgeType)
    {
        return metaForgeType.ToLowerInvariant() switch
        {
            "string" => typeof(string),
            "text" => typeof(string),
            "int" => typeof(int),
            "integer" => typeof(int),
            "decimal" => typeof(decimal),
            "bool" => typeof(bool),
            "boolean" => typeof(bool),
            "date" => typeof(DateTime),
            "datetime" => typeof(DateTime),
            "time" => typeof(TimeSpan),
            "jsonb" => typeof(string),
            "json" => typeof(string),
            "uuid" => typeof(Guid),
            "binary" => typeof(byte[]),
            _ => typeof(string)
        };
    }

    /// <summary>
    /// Mapea la acción de eliminación de MetaForge a EF Core
    /// </summary>
    private static DeleteBehavior MapDeleteBehavior(string onDelete)
    {
        return onDelete.ToUpperInvariant() switch
        {
            "CASCADE" => DeleteBehavior.Cascade,
            "RESTRICT" => DeleteBehavior.Restrict,
            "SETNULL" => DeleteBehavior.SetNull,
            "NOACTION" => DeleteBehavior.NoAction,
            _ => DeleteBehavior.Restrict
        };
    }
}
