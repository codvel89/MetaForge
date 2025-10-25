namespace MetaForge.Shared;

/// <summary>
/// Define los tipos de relaciones entre tablas
/// </summary>
public enum RelationshipType
{
    /// <summary>
    /// Relación Uno a Uno
    /// </summary>
    OneToOne,

    /// <summary>
    /// Relación Uno a Muchos
    /// </summary>
    OneToMany,

    /// <summary>
    /// Relación Muchos a Uno
    /// </summary>
    ManyToOne,

    /// <summary>
    /// Relación Muchos a Muchos
    /// </summary>
    ManyToMany
}
