using Contacts.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using Reports.Worker.Events;
using System.Text;
using System.Text.Json;

namespace Reports.Worker.Services;

public class RabbitMqEventSubscriberService : BackgroundService
{
    private readonly ILogger<RabbitMqEventSubscriberService> _logger;
    private readonly RabbitMqSettings _settings;
    private IConnection _connection = null!;
    private IModel _channel = null!;
    private const string _queueName = "report";

    public RabbitMqEventSubscriberService(
        ILogger<RabbitMqEventSubscriberService> logger,
        IOptions<RabbitMqSettings> settings)
    {
        _settings = settings.Value;
        _logger = logger;
    }

    public override Task StartAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            DispatchConsumersAsync = true,
            HostName = _settings.HostName,
            Port = _settings.Port,
            UserName = _settings.UserName,
            Password = _settings.Password
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclarePassive(_queueName);
        _channel.BasicQos(0, 1, false);

        _logger.LogInformation("Queue [{_queueName}] is waiting for messages.", _queueName);

        return base.StartAsync(stoppingToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (bc, ea) =>
        {
            var message = Encoding.UTF8.GetString(ea.Body.ToArray());
            _logger.LogInformation("Processing msg: '{message}'.", message);
            try
            {
                _ = JsonSerializer.Deserialize<ReportRequestReceivedEvent>(message);
                _logger.LogInformation("Received ReportRequestReceivedEvent {message}", message);

                await Task.Delay(new Random().Next(1, 3) * 1000, stoppingToken); // simulate an async process

                _channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (JsonException)
            {
                _logger.LogError("JSON Parse Error: '{message}'.", message);
                _channel.BasicNack(ea.DeliveryTag, false, false);
            }
            catch (AlreadyClosedException)
            {
                _logger.RabbitMqConnectionClosed();
            }
            catch (Exception e)
            {
                _logger.ErrorThrown(e, e.Message);
            }
        };

        _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);

        await Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
        _connection.Close();
        _logger.RabbitMqConnectionClosed();
    }
}

public static class LoggerExtensions
{
    private static readonly Action<ILogger, Exception> _rabbitMqConnectionClosed = LoggerMessage.Define(
        LogLevel.Information,
        new EventId(1, nameof(RabbitMqConnectionClosed)),
        "RabbitMQ connection is closed.");

    private static readonly Action<ILogger, string, Exception> _errorThrown = LoggerMessage.Define<string>(
        LogLevel.Information,
        new EventId(2, nameof(RabbitMqConnectionClosed)),
        "Unknown Error");

    public static void RabbitMqConnectionClosed(this ILogger logger)
    {
        _rabbitMqConnectionClosed(logger, null);
    }

    public static void ErrorThrown(this ILogger logger, Exception exception, string message)
    {
        _errorThrown(logger, message, exception);
    }
}
