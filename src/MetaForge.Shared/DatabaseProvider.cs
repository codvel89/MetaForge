namespace MetaForge.Shared;

/// <summary>
/// Define los proveedores de bases de datos soportados por el sistema
/// </summary>
public enum DatabaseProvider
{
    /// <summary>
    /// PostgreSQL
    /// </summary>
    PostgreSQL,

    /// <summary>
    /// MySQL/MariaDB
    /// </summary>
    MySQL,

    /// <summary>
    /// Microsoft SQL Server
    /// </summary>
    SQLServer,

    /// <summary>
    /// SQLite
    /// </summary>
    SQLite,

    /// <summary>
    /// Oracle Database
    /// </summary>
    Oracle
}
