using Contacts.Api.Filters;
using Contacts.Api.PipelineBehaviors;
using Contacts.Application;
using Contacts.Infrastructure;
using MediatR;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console());

var services = builder.Services;
var configuration = builder.Configuration;
var env = builder.Environment;

services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

services.AddApplicationServices(configuration);
services.AddInfrastructureServices(configuration);

services.AddControllers(options =>
    options.Filters.Add<CustomExceptionFilter>()
);
services.AddLogging(config => config.AddConsole().AddConfiguration(configuration.GetSection("Logging")));

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Contacts API", Version = "v1"
    });
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Contacts.Api.xml"));
});

void App()
{
    var app = builder.Build();

    app.UseSwagger();

    app.UseRouting();
    app.MapControllers();
    app.UseSerilogRequestLogging();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contacts API V1");
        c.RoutePrefix = "";
        c.DocExpansion(DocExpansion.None);
    });

    app.Run();
}

App();
