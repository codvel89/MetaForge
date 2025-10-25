namespace MetaForge.Shared;

/// <summary>
/// Configuración para generar secuencias automáticas
/// </summary>
public class SequenceConfig
{
    /// <summary>
    /// Prefijo de la secuencia
    /// </summary>
    public string Prefix { get; set; } = string.Empty;

    /// <summary>
    /// Formato del año (yyyy, yy)
    /// </summary>
    public string YearFormat { get; set; } = string.Empty;

    /// <summary>
    /// Longitud del número secuencial
    /// </summary>
    public int NumberLength { get; set; }

    /// <summary>
    /// Patrón completo de la secuencia (ej: {Prefix}-{Year}-{Number})
    /// </summary>
    public string Pattern { get; set; } = string.Empty;
}
