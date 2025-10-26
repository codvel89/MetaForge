namespace MetaForge.Core.Services.Audit;

/// <summary>
/// Servicio de auditoría para registro de cambios
/// </summary>
public interface IAuditService
{
    /// <summary>
    /// Registra una operación de creación
    /// </summary>
    /// <param name="entityName">Nombre de la entidad</param>
    /// <param name="entityId">ID de la entidad</param>
    /// <param name="newValues">Valores nuevos en JSON</param>
    /// <param name="performedBy">Usuario que realizó la acción</param>
    /// <param name="ipAddress">IP del cliente</param>
    /// <param name="userAgent">User agent</param>
    Task LogCreateAsync(string entityName, string entityId, string newValues, string? performedBy = null, string? ipAddress = null, string? userAgent = null);

    /// <summary>
    /// Registra una operación de actualización
    /// </summary>
    /// <param name="entityName">Nombre de la entidad</param>
    /// <param name="entityId">ID de la entidad</param>
    /// <param name="oldValues">Valores anteriores en JSON</param>
    /// <param name="newValues">Valores nuevos en JSON</param>
    /// <param name="performedBy">Usuario que realizó la acción</param>
    /// <param name="ipAddress">IP del cliente</param>
    /// <param name="userAgent">User agent</param>
    Task LogUpdateAsync(string entityName, string entityId, string oldValues, string newValues, string? performedBy = null, string? ipAddress = null, string? userAgent = null);

    /// <summary>
    /// Registra una operación de eliminación
    /// </summary>
    /// <param name="entityName">Nombre de la entidad</param>
    /// <param name="entityId">ID de la entidad</param>
    /// <param name="oldValues">Valores eliminados en JSON</param>
    /// <param name="performedBy">Usuario que realizó la acción</param>
    /// <param name="ipAddress">IP del cliente</param>
    /// <param name="userAgent">User agent</param>
    Task LogDeleteAsync(string entityName, string entityId, string oldValues, string? performedBy = null, string? ipAddress = null, string? userAgent = null);

    /// <summary>
    /// Obtiene el historial de auditoría de una entidad
    /// </summary>
    /// <param name="entityName">Nombre de la entidad</param>
    /// <param name="entityId">ID de la entidad</param>
    /// <param name="skip">Número de registros a saltar</param>
    /// <param name="take">Número de registros a tomar</param>
    /// <returns>Lista de registros de auditoría</returns>
    Task<List<Entities.Audit.AuditLog>> GetEntityHistoryAsync(string entityName, string entityId, int skip = 0, int take = 50);

    /// <summary>
    /// Obtiene el historial de auditoría por usuario
    /// </summary>
    /// <param name="performedBy">Usuario que realizó las acciones</param>
    /// <param name="skip">Número de registros a saltar</param>
    /// <param name="take">Número de registros a tomar</param>
    /// <returns>Lista de registros de auditoría</returns>
    Task<List<Entities.Audit.AuditLog>> GetUserHistoryAsync(string performedBy, int skip = 0, int take = 50);
}
