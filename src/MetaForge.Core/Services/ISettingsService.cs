namespace MetaForge.Core.Services;

/// <summary>
/// Servicio para leer configuraciones del sistema desde SystemSettings
/// </summary>
public interface ISettingsService
{
    /// <summary>
    /// Obtiene un valor de configuración
    /// </summary>
    /// <param name="key">Clave de configuración</param>
    /// <param name="defaultValue">Valor por defecto si no existe</param>
    /// <returns>Valor de configuración</returns>
    Task<string> GetSettingAsync(string key, string defaultValue = "");

    /// <summary>
    /// Obtiene un valor de configuración como tipo específico
    /// </summary>
    Task<T> GetSettingAsync<T>(string key, T defaultValue = default!);

    /// <summary>
    /// Establece un valor de configuración
    /// </summary>
    Task SetSettingAsync(string key, string value, string? category = null, string? description = null);

    /// <summary>
    /// Verifica si una configuración existe
    /// </summary>
    Task<bool> ExistsAsync(string key);

    /// <summary>
    /// Elimina una configuración
    /// </summary>
    Task DeleteSettingAsync(string key);

    /// <summary>
    /// Obtiene todas las configuraciones de una categoría
    /// </summary>
    Task<Dictionary<string, string>> GetCategorySettingsAsync(string category);
}
