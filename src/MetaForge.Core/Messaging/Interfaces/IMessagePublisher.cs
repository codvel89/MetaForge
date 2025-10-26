namespace MetaForge.Core.Messaging.Interfaces;

/// <summary>
/// Interface para publicar mensajes en colas de RabbitMQ
/// </summary>
public interface IMessagePublisher
{
    /// <summary>
    /// Publica un mensaje a una cola específica
    /// </summary>
    /// <typeparam name="T">Tipo del mensaje</typeparam>
    /// <param name="queueName">Nombre de la cola de destino</param>
    /// <param name="message">Mensaje a publicar</param>
    /// <param name="priority">Prioridad del mensaje (0-10, donde 10 es mayor prioridad)</param>
    /// <param name="correlationId">Identificador de correlación para rastreo</param>
    Task PublishAsync<T>(string queueName, T message, byte priority = 5, string? correlationId = null) where T : class;

    /// <summary>
    /// Publica múltiples mensajes a una cola específica
    /// </summary>
    /// <typeparam name="T">Tipo de los mensajes</typeparam>
    /// <param name="queueName">Nombre de la cola de destino</param>
    /// <param name="messages">Lista de mensajes a publicar</param>
    /// <param name="priority">Prioridad de los mensajes (0-10, donde 10 es mayor prioridad)</param>
    Task PublishBatchAsync<T>(string queueName, IEnumerable<T> messages, byte priority = 5) where T : class;

    /// <summary>
    /// Publica un mensaje con tiempo de vida (TTL)
    /// </summary>
    /// <typeparam name="T">Tipo del mensaje</typeparam>
    /// <param name="queueName">Nombre de la cola de destino</param>
    /// <param name="message">Mensaje a publicar</param>
    /// <param name="ttlSeconds">Tiempo de vida del mensaje en segundos</param>
    /// <param name="priority">Prioridad del mensaje (0-10, donde 10 es mayor prioridad)</param>
    /// <param name="correlationId">Identificador de correlación para rastreo</param>
    Task PublishWithTtlAsync<T>(string queueName, T message, int ttlSeconds, byte priority = 5, string? correlationId = null) where T : class;
}
