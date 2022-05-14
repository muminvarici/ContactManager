using MediatR;
using Reports.Worker.Events.ExcelDataProduced;
using Reports.Worker.Models;
using Reports.Worker.Services.ApiClients;

namespace Reports.Worker.Events.ReportRequestReceived;

public class ReportRequestReceivedEventHandler : INotificationHandler<ReportRequestReceivedEvent>
{
    private ILogger<ReportRequestReceivedEventHandler> _logger;
    private readonly IContactsApi _contactsApi;
    private readonly IPublisher _publisher;

    public ReportRequestReceivedEventHandler(
        ILogger<ReportRequestReceivedEventHandler> logger,
        IContactsApi contactsApi,
        IPublisher publisher
    )
    {
        _logger = logger;
        _contactsApi = contactsApi;
        _publisher = publisher;
    }

    public async Task Handle(ReportRequestReceivedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event is handled by worker event handler");
        var response = await _contactsApi.GetContacts(true);
        if (!response.IsSuccess || response.Data == null)
        {
            _logger.LogWarning("No data to generate report");
            return;
        }

        var distinctLocations = response.Data
            .SelectMany(w => w.AdditionalInfo?.Where(q => q.Type == AdditionalInfoType.Location))
            .Select(w => w.Value)
            .Distinct();
        var data = distinctLocations.Select(location =>
        {
            var locationContacts = response.Data
                .Where(contact => contact.AdditionalInfo?
                    .Any(info => info.Type == AdditionalInfoType.Location && info.Value == location) ?? false).ToList();

            var phoneCount = locationContacts.Sum(contact => contact.AdditionalInfo.Count(info => info.Type == AdditionalInfoType.Phone));

            return new ExcelRow
            {
                Location = location, ContactCount = locationContacts.Count, PhoneCount = phoneCount
            };
        });

        await _publisher.Publish(new ExcelDataProducedEvent
        {
            Id=notification.Id,
            Data = data
        }, cancellationToken);
    }
}
