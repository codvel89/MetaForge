namespace MetaForge.Shared.Models;

/// <summary>
/// Representa un esquema de base de datos
/// </summary>
public class DatabaseSchema
{
    /// <summary>
    /// Nombre del esquema
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Identificador de la conexión de base de datos
    /// </summary>
    public int DatabaseConnectionId { get; set; }

    /// <summary>
    /// Indica si es el esquema predeterminado
    /// </summary>
    public bool IsDefault { get; set; }

    /// <summary>
    /// Descripción del esquema
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Lista de tablas en el esquema
    /// </summary>
    public List<string>? Tables { get; set; }
}
