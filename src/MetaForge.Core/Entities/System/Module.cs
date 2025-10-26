namespace MetaForge.Core.Entities.System;

/// <summary>
/// Representa un módulo del sistema (instalado o disponible)
/// </summary>
public class Module
{
    /// <summary>
    /// Identificador único del módulo
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre del módulo
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Nombre visible del módulo
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del módulo
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Versión del módulo
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Autor del módulo
    /// </summary>
    public string? Author { get; set; }

    /// <summary>
    /// Indica si el módulo está instalado
    /// </summary>
    public bool IsInstalled { get; set; }

    /// <summary>
    /// Indica si el módulo está activo
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de instalación
    /// </summary>
    public DateTime? InstalledAt { get; set; }

    /// <summary>
    /// Fecha de última actualización
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Path del assembly del módulo
    /// </summary>
    public string? AssemblyPath { get; set; }

    /// <summary>
    /// Dependencias del módulo
    /// </summary>
    public List<ModuleDependency> Dependencies { get; set; } = new();
}
