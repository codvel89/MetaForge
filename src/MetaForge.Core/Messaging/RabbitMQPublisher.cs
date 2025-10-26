using System.Text;
using System.Text.Json;
using MetaForge.Core.Messaging.Interfaces;
using RabbitMQ.Client;

namespace MetaForge.Core.Messaging;

/// <summary>
/// Implementación de IMessagePublisher para RabbitMQ
/// </summary>
public class RabbitMQPublisher : IMessagePublisher, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly JsonSerializerOptions _jsonOptions;
    private bool _disposed;

    /// <summary>
    /// Constructor que inicializa la conexión a RabbitMQ
    /// </summary>
    public RabbitMQPublisher()
    {
        var factory = new ConnectionFactory
        {
            HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost",
            Port = int.Parse(Environment.GetEnvironmentVariable("RABBITMQ_PORT") ?? "5672"),
            UserName = Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? "guest",
            Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest",
            VirtualHost = Environment.GetEnvironmentVariable("RABBITMQ_VHOST") ?? "/",
            DispatchConsumersAsync = true
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };
    }

    /// <summary>
    /// Publica un mensaje a una cola específica
    /// </summary>
    public Task PublishAsync<T>(string queueName, T message, byte priority = 5, string? correlationId = null) where T : class
    {
        EnsureQueueExists(queueName);

        var body = SerializeMessage(message);
        var properties = _channel.CreateBasicProperties();
        properties.Persistent = true;
        properties.Priority = priority;
        properties.CorrelationId = correlationId ?? Guid.NewGuid().ToString();
        properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        properties.ContentType = "application/json";
        properties.ContentEncoding = "utf-8";

        _channel.BasicPublish(
            exchange: string.Empty,
            routingKey: queueName,
            mandatory: false,
            basicProperties: properties,
            body: body
        );

        return Task.CompletedTask;
    }

    /// <summary>
    /// Publica múltiples mensajes a una cola específica
    /// </summary>
    public Task PublishBatchAsync<T>(string queueName, IEnumerable<T> messages, byte priority = 5) where T : class
    {
        EnsureQueueExists(queueName);

        foreach (var message in messages)
        {
            PublishAsync(queueName, message, priority).Wait();
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Publica un mensaje con tiempo de vida (TTL)
    /// </summary>
    public Task PublishWithTtlAsync<T>(string queueName, T message, int ttlSeconds, byte priority = 5, string? correlationId = null) where T : class
    {
        EnsureQueueExists(queueName);

        var body = SerializeMessage(message);
        var properties = _channel.CreateBasicProperties();
        properties.Persistent = true;
        properties.Priority = priority;
        properties.CorrelationId = correlationId ?? Guid.NewGuid().ToString();
        properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        properties.ContentType = "application/json";
        properties.ContentEncoding = "utf-8";
        properties.Expiration = (ttlSeconds * 1000).ToString(); // TTL en milisegundos

        _channel.BasicPublish(
            exchange: string.Empty,
            routingKey: queueName,
            mandatory: false,
            basicProperties: properties,
            body: body
        );

        return Task.CompletedTask;
    }

    /// <summary>
    /// Asegura que la cola existe, creándola si es necesario
    /// </summary>
    private void EnsureQueueExists(string queueName)
    {
        _channel.QueueDeclare(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: new Dictionary<string, object>
            {
                { "x-max-priority", 10 } // Soporte para prioridades 0-10
            }
        );
    }

    /// <summary>
    /// Serializa el mensaje a JSON
    /// </summary>
    private byte[] SerializeMessage<T>(T message) where T : class
    {
        var json = JsonSerializer.Serialize(message, _jsonOptions);
        return Encoding.UTF8.GetBytes(json);
    }

    /// <summary>
    /// Libera los recursos
    /// </summary>
    public void Dispose()
    {
        if (_disposed) return;

        _channel?.Dispose();
        _connection?.Dispose();
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}
