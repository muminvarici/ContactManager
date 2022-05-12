using Contacts.Domain.Events.Abstractions;
using Contacts.Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Contacts.Infrastructure.Events.Concretes;

public class RabbitMqEventPublisher : IEventPublisher
{
    private readonly ILogger<RabbitMqEventPublisher> _logger;
    private readonly IModel _channel;
    private const string _queueName = "report";

    public RabbitMqEventPublisher(
        IOptions<RabbitMqSettings> settings,
        ILogger<RabbitMqEventPublisher> logger
    )
    {
        _logger = logger;
        _logger.LogWarning("Connecting to rabbitmq");

        //todo move to factory class
        var factory = new ConnectionFactory
        {
            HostName = settings.Value.HostName, Port = settings.Value.Port, UserName = settings.Value.UserName, Password = settings.Value.Password
        };

        var conn = factory.CreateConnection();
        _channel = conn.CreateModel();
        _channel.QueueDeclare(queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    public bool Enqueue(object eventMessage)
    {
        var message = JsonSerializer.Serialize(eventMessage);
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: "",
            routingKey: _queueName,
            basicProperties: null,
            body: body);
        _logger.LogWarning("Event published route:{route} message: {message} to RabbitMQ",
            _queueName, message);
        return true;
    }
}
