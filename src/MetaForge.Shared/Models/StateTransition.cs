namespace MetaForge.Shared.Models;

/// <summary>
/// Define las transiciones permitidas entre estados
/// </summary>
public class StateTransition
{
    /// <summary>
    /// Estado origen
    /// </summary>
    public string From { get; set; } = string.Empty;

    /// <summary>
    /// Estados destino permitidos
    /// </summary>
    public string[] To { get; set; } = Array.Empty<string>();
}
