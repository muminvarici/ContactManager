using FluentValidation;
using MediatR;
using Refit;
using Reports.Worker.Services;
using Reports.Worker.Services.ApiClients;
using Reports.Worker.Settings;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHostedService<RabbitMqEventSubscriberService>();

builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection(nameof(RabbitMqSettings)));

builder.Services.AddMediatR(typeof(Program));

builder.Services.Configure<RefitSettings>(builder.Configuration.GetSection(nameof(RefitSettings)));

builder.Services.AddRefitClient<IContactsApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceUrls:ContactsApi")));
;


builder.Services.AddRefitClient<IReportsApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceUrls:ReportsApi")));
;


var app = builder.Build();
app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.Run();
