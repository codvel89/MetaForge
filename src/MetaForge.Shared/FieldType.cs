namespace MetaForge.Shared;

/// <summary>
/// Define los tipos de datos para campos de tablas
/// </summary>
public enum FieldType
{
    /// <summary>
    /// Cadena de texto
    /// </summary>
    String,

    /// <summary>
    /// Texto largo
    /// </summary>
    Text,

    /// <summary>
    /// Número entero
    /// </summary>
    Integer,

    /// <summary>
    /// Número decimal
    /// </summary>
    Decimal,

    /// <summary>
    /// Valor booleano (verdadero/falso)
    /// </summary>
    Boolean,

    /// <summary>
    /// Fecha
    /// </summary>
    Date,

    /// <summary>
    /// Fecha y hora
    /// </summary>
    DateTime,

    /// <summary>
    /// Hora
    /// </summary>
    Time,

    /// <summary>
    /// JSON binario (PostgreSQL)
    /// </summary>
    JsonB,

    /// <summary>
    /// JSON
    /// </summary>
    Json,

    /// <summary>
    /// UUID/GUID
    /// </summary>
    Uuid,

    /// <summary>
    /// Datos binarios
    /// </summary>
    Binary,

    /// <summary>
    /// Enum personalizado
    /// </summary>
    Enum
}
