namespace MetaForge.Core.Entities.System;

/// <summary>
/// Representa una configuración global del sistema
/// </summary>
public class SystemSetting
{
    /// <summary>
    /// Identificador único
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Clave de la configuración
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Valor de la configuración
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Descripción de la configuración
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Categoría de la configuración
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Tipo de dato (string, int, bool, json)
    /// </summary>
    public string DataType { get; set; } = "string";

    /// <summary>
    /// Indica si es una configuración del sistema (no editable)
    /// </summary>
    public bool IsSystem { get; set; }

    /// <summary>
    /// Fecha de última modificación
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Usuario que modificó (opcional)
    /// </summary>
    public int? UpdatedBy { get; set; }
}
