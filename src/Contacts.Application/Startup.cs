using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contacts.Application;

public static class Startup
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var type = typeof(Startup);
        return services.AddMediatR(type)
            .AddValidatorsFromAssembly(type.Assembly);
    }
}
