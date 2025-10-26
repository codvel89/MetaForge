using System.Text.Json;
using MetaForge.Core.Context;
using MetaForge.Core.Entities.System;
using Microsoft.EntityFrameworkCore;

namespace MetaForge.Core.Services;

/// <summary>
/// Implementación del servicio de configuraciones con caché en memoria
/// </summary>
public class SettingsService : ISettingsService
{
    private readonly MetadataDbContext _context;
    private readonly Dictionary<string, string> _cache = new();
    private DateTime _lastCacheUpdate = DateTime.MinValue;
    private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(5);

    /// <summary>
    /// Constructor
    /// </summary>
    public SettingsService(MetadataDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene un valor de configuración
    /// </summary>
    public async Task<string> GetSettingAsync(string key, string defaultValue = "")
    {
        await RefreshCacheIfNeededAsync();

        return _cache.TryGetValue(key, out var value) ? value : defaultValue;
    }

    /// <summary>
    /// Obtiene un valor de configuración como tipo específico
    /// </summary>
    public async Task<T> GetSettingAsync<T>(string key, T defaultValue = default!)
    {
        var stringValue = await GetSettingAsync(key, "");
        
        if (string.IsNullOrEmpty(stringValue))
            return defaultValue;

        try
        {
            if (typeof(T) == typeof(string))
                return (T)(object)stringValue;
            
            if (typeof(T) == typeof(int))
                return (T)(object)int.Parse(stringValue);
            
            if (typeof(T) == typeof(bool))
                return (T)(object)bool.Parse(stringValue);
            
            if (typeof(T) == typeof(double))
                return (T)(object)double.Parse(stringValue);

            // Para tipos complejos, deserializar JSON
            return JsonSerializer.Deserialize<T>(stringValue) ?? defaultValue;
        }
        catch
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// Establece un valor de configuración
    /// </summary>
    public async Task SetSettingAsync(string key, string value, string? category = null, string? description = null)
    {
        var setting = await _context.SystemSettings.FirstOrDefaultAsync(s => s.Key == key);

        if (setting == null)
        {
            setting = new SystemSetting
            {
                Key = key,
                Value = value,
                Category = category,
                Description = description,
                IsSystem = false,
                UpdatedAt = DateTime.UtcNow
            };
            _context.SystemSettings.Add(setting);
        }
        else
        {
            setting.Value = value;
            if (category != null) setting.Category = category;
            if (description != null) setting.Description = description;
            setting.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();

        // Actualizar caché
        _cache[key] = value;
    }

    /// <summary>
    /// Verifica si una configuración existe
    /// </summary>
    public async Task<bool> ExistsAsync(string key)
    {
        await RefreshCacheIfNeededAsync();
        return _cache.ContainsKey(key);
    }

    /// <summary>
    /// Elimina una configuración
    /// </summary>
    public async Task DeleteSettingAsync(string key)
    {
        var setting = await _context.SystemSettings.FirstOrDefaultAsync(s => s.Key == key);
        
        if (setting != null)
        {
            _context.SystemSettings.Remove(setting);
            await _context.SaveChangesAsync();
            _cache.Remove(key);
        }
    }

    /// <summary>
    /// Obtiene todas las configuraciones de una categoría
    /// </summary>
    public async Task<Dictionary<string, string>> GetCategorySettingsAsync(string category)
    {
        await RefreshCacheIfNeededAsync();

        return _cache
            .Where(kvp => kvp.Key.StartsWith($"{category}."))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }

    /// <summary>
    /// Refresca el caché si ha expirado
    /// </summary>
    private async Task RefreshCacheIfNeededAsync()
    {
        if (DateTime.UtcNow - _lastCacheUpdate < _cacheExpiration && _cache.Any())
            return;

        var settings = await _context.SystemSettings.ToListAsync();
        
        _cache.Clear();
        foreach (var setting in settings)
        {
            _cache[setting.Key] = setting.Value;
        }

        _lastCacheUpdate = DateTime.UtcNow;
    }
}
