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
        _channel.QueueDeclare(queue: "report",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    public bool Enqueue(string route, object eventMessage)
    {
        var message = JsonSerializer.Serialize(eventMessage);
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: "",
            routingKey: route,
            basicProperties: null,
            body: body);
        _logger.LogWarning("Event published route:{route} message: {message} to RabbitMQ",
            route, message);
        return true;
    }
}
