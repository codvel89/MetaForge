namespace MetaForge.Shared;

/// <summary>
/// Representa una opción para controles de selección (Select, Radio, etc.)
/// </summary>
public class SelectOption
{
    /// <summary>
    /// Valor de la opción
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Etiqueta visible de la opción
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Color asociado a la opción (opcional)
    /// </summary>
    public string? Color { get; set; }
}
