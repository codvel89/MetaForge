using MetaForge.Core.Entities.Notification;

namespace MetaForge.Core.Repositories;

public interface INotificationTemplateRepository
{
    Task<NotificationTemplate?> GetByIdAsync(int id);
    Task<NotificationTemplate?> GetByCodeAsync(string code);
    Task<List<NotificationTemplate>> GetAllAsync(bool activeOnly = false);
    Task<List<NotificationTemplate>> GetByCategoryAsync(string category);
    Task<NotificationTemplate> CreateAsync(NotificationTemplate template);
    Task<NotificationTemplate> UpdateAsync(NotificationTemplate template);
    Task DeleteAsync(int id);
}

public interface IEmailTemplateRepository
{
    Task<EmailTemplate?> GetByIdAsync(int id);
    Task<EmailTemplate?> GetByCodeAsync(string code);
    Task<List<EmailTemplate>> GetAllAsync(bool activeOnly = false);
    Task<List<EmailTemplate>> GetByCategoryAsync(string category);
    Task<EmailTemplate> CreateAsync(EmailTemplate template);
    Task<EmailTemplate> UpdateAsync(EmailTemplate template);
    Task DeleteAsync(int id);
}
