namespace MetaForge.Core.Entities.System;

/// <summary>
/// Representa una dependencia entre módulos
/// </summary>
public class ModuleDependency
{
    /// <summary>
    /// Identificador único
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID del módulo que tiene la dependencia
    /// </summary>
    public int ModuleId { get; set; }

    /// <summary>
    /// Nombre del módulo requerido
    /// </summary>
    public string RequiredModuleName { get; set; } = string.Empty;

    /// <summary>
    /// Versión mínima requerida
    /// </summary>
    public string? MinVersion { get; set; }

    /// <summary>
    /// Indica si es una dependencia obligatoria
    /// </summary>
    public bool IsRequired { get; set; } = true;

    /// <summary>
    /// Módulo padre
    /// </summary>
    public Module Module { get; set; } = null!;
}
