namespace MetaForge.Core.Entities.System;

/// <summary>
/// Representa una migración de base de datos ejecutada
/// </summary>
public class Migration
{
    /// <summary>
    /// Identificador único de la migración
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre de la migración
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Versión de la migración
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Fecha y hora de ejecución
    /// </summary>
    public DateTime ExecutedAt { get; set; }

    /// <summary>
    /// Identificador de la conexión donde se ejecutó
    /// </summary>
    public int? DatabaseConnectionId { get; set; }

    /// <summary>
    /// SQL ejecutado (opcional)
    /// </summary>
    public string? SqlScript { get; set; }

    /// <summary>
    /// Indica si fue exitosa
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Mensaje de error si falló
    /// </summary>
    public string? ErrorMessage { get; set; }
}
