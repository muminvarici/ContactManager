using MediatR;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using Reports.Worker.Events;
using Reports.Worker.Events.ReportRequestReceived;
using Reports.Worker.Settings;
using System.Text;
using System.Text.Json;

namespace Reports.Worker.Services;

public class RabbitMqEventSubscriberService : BackgroundService
{
    private readonly ILogger<RabbitMqEventSubscriberService> _logger;
    private readonly IPublisher _publisher;
    private readonly RabbitMqSettings _settings;
    private IConnection _connection = null!;
    private IModel _channel = null!;
    private const string _queueName = "report";

    public RabbitMqEventSubscriberService(
        ILogger<RabbitMqEventSubscriberService> logger,
        IOptions<RabbitMqSettings> settings,
        IPublisher publisher
        )
    {
        _settings = settings.Value;
        _logger = logger;
        _publisher = publisher;
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
            _logger.LogInformation("Received msg: '{message}'.", message);
            try
            {
                var eventMessage = JsonSerializer.Deserialize<ReportRequestReceivedEvent>(message);

                await _publisher.Publish(eventMessage, stoppingToken);

                _channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (JsonException)
            {
                _logger.LogError("JSON Parse Error: '{message}'.", message);
                _channel.BasicNack(ea.DeliveryTag, false, false);
            }
            catch (AlreadyClosedException)
            {
                _logger.LogError("RabbitMQ connection is closed.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        };

        _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);

        await Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
        _connection.Close();
        _logger.LogError("Unknown Error");
    }
}
