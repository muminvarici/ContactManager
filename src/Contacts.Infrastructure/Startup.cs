using Contacts.Domain.Events.Abstractions;
using Contacts.Domain.Repositories.Abstracts;
using Contacts.Infrastructure.Events.Concretes;
using Contacts.Infrastructure.Repositories.Concretes;
using Contacts.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contacts.Infrastructure;

public static class Startup
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DbSettings>(configuration.GetSection(nameof(DbSettings)));
        services.Configure<RabbitMqSettings>(configuration.GetSection(nameof(RabbitMqSettings)));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddSingleton<IEventPublisher, RabbitMqEventPublisher>();
    }
}
