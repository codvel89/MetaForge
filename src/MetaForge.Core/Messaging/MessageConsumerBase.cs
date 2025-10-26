using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MetaForge.Core.Messaging;

/// <summary>
/// Clase base abstracta para consumidores de mensajes de RabbitMQ
/// </summary>
/// <typeparam name="TMessage">Tipo del mensaje a consumir</typeparam>
public abstract class MessageConsumerBase<TMessage> : IDisposable where TMessage : class
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;
    private readonly JsonSerializerOptions _jsonOptions;
    private bool _disposed;
    private bool _isConsuming;

    /// <summary>
    /// Constructor que inicializa el consumidor
    /// </summary>
    /// <param name="queueName">Nombre de la cola a consumir</param>
    protected MessageConsumerBase(string queueName)
    {
        _queueName = queueName;

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
            PropertyNameCaseInsensitive = true
        };

        EnsureQueueExists();
    }

    /// <summary>
    /// Inicia el consumo de mensajes de la cola
    /// </summary>
    public async Task StartConsumingAsync(CancellationToken cancellationToken = default)
    {
        if (_isConsuming)
            throw new InvalidOperationException("El consumidor ya está ejecutándose");

        _isConsuming = true;

        // Configurar QoS para procesar un mensaje a la vez
        _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (sender, eventArgs) =>
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            try
            {
                var message = DeserializeMessage(eventArgs.Body.ToArray());
                if (message != null)
                {
                    await ProcessMessageAsync(message, eventArgs.BasicProperties.CorrelationId);
                    _channel.BasicAck(eventArgs.DeliveryTag, multiple: false);
                }
                else
                {
                    // Mensaje inválido, rechazar sin requeue
                    _channel.BasicNack(eventArgs.DeliveryTag, multiple: false, requeue: false);
                }
            }
            catch (Exception ex)
            {
                await OnErrorAsync(ex, eventArgs);
                
                // Rechazar y reencolar el mensaje
                _channel.BasicNack(eventArgs.DeliveryTag, multiple: false, requeue: true);
            }
        };

        _channel.BasicConsume(
            queue: _queueName,
            autoAck: false,
            consumer: consumer
        );

        await OnConsumerStartedAsync();
    }

    /// <summary>
    /// Detiene el consumo de mensajes
    /// </summary>
    public async Task StopConsumingAsync()
    {
        _isConsuming = false;
        await OnConsumerStoppedAsync();
    }

    /// <summary>
    /// Método abstracto que debe implementar la lógica de procesamiento del mensaje
    /// </summary>
    /// <param name="message">Mensaje deserializado</param>
    /// <param name="correlationId">Identificador de correlación</param>
    protected abstract Task ProcessMessageAsync(TMessage message, string? correlationId);

    /// <summary>
    /// Método virtual que se ejecuta cuando ocurre un error al procesar un mensaje
    /// </summary>
    protected virtual Task OnErrorAsync(Exception exception, BasicDeliverEventArgs eventArgs)
    {
        Console.Error.WriteLine($"Error procesando mensaje en cola '{_queueName}': {exception.Message}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// Método virtual que se ejecuta cuando el consumidor inicia
    /// </summary>
    protected virtual Task OnConsumerStartedAsync()
    {
        Console.WriteLine($"Consumidor iniciado para cola '{_queueName}'");
        return Task.CompletedTask;
    }

    /// <summary>
    /// Método virtual que se ejecuta cuando el consumidor se detiene
    /// </summary>
    protected virtual Task OnConsumerStoppedAsync()
    {
        Console.WriteLine($"Consumidor detenido para cola '{_queueName}'");
        return Task.CompletedTask;
    }

    /// <summary>
    /// Asegura que la cola existe
    /// </summary>
    private void EnsureQueueExists()
    {
        _channel.QueueDeclare(
            queue: _queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: new Dictionary<string, object>
            {
                { "x-max-priority", 10 }
            }
        );
    }

    /// <summary>
    /// Deserializa el mensaje desde JSON
    /// </summary>
    private TMessage? DeserializeMessage(byte[] body)
    {
        try
        {
            var json = Encoding.UTF8.GetString(body);
            return JsonSerializer.Deserialize<TMessage>(json, _jsonOptions);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Libera los recursos
    /// </summary>
    public void Dispose()
    {
        if (_disposed) return;

        _isConsuming = false;
        _channel?.Dispose();
        _connection?.Dispose();
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}
