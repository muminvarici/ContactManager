using MediatR;

namespace Contacts.Application.Events.Reports;

public class ReportRequestReceivedEvent : INotification
{
    public string Id { get; set; }
}
