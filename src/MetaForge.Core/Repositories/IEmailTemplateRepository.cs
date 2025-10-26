using MetaForge.Core.Entities.Notification;

namespace MetaForge.Core.Repositories;

/// <summary>
/// Repositorio para gestionar plantillas de correo electrónico
/// </summary>
public interface IEmailTemplateRepository
{
    /// <summary>
    /// Obtiene una plantilla por su identificador
    /// </summary>
    Task<EmailTemplate?> GetByIdAsync(int id);

    /// <summary>
    /// Obtiene una plantilla por su código único
    /// </summary>
    Task<EmailTemplate?> GetByCodeAsync(string code);

    /// <summary>
    /// Obtiene todas las plantillas, opcionalmente solo las activas
    /// </summary>
    Task<List<EmailTemplate>> GetAllAsync(bool activeOnly = false);

    /// <summary>
    /// Obtiene plantillas por categoría
    /// </summary>
    Task<List<EmailTemplate>> GetByCategoryAsync(string category);

    /// <summary>
    /// Crea una nueva plantilla
    /// </summary>
    Task<EmailTemplate> CreateAsync(EmailTemplate template);

    /// <summary>
    /// Actualiza una plantilla existente
    /// </summary>
    Task<EmailTemplate> UpdateAsync(EmailTemplate template);

    /// <summary>
    /// Elimina una plantilla por su identificador
    /// </summary>
    Task DeleteAsync(int id);
}
