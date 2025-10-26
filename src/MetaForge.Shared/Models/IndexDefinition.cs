namespace MetaForge.Shared.Models;

/// <summary>
/// Define un índice de base de datos para una tabla
/// </summary>
public class IndexDefinition
{
    /// <summary>
    /// Nombre del índice
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Columnas incluidas en el índice
    /// </summary>
    public string[] Columns { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Indica si es un índice único
    /// </summary>
    public bool IsUnique { get; set; }

    /// <summary>
    /// Tipo de índice (BTree, Hash, GIN, GIST, etc.)
    /// </summary>
    public string? IndexType { get; set; }
}
