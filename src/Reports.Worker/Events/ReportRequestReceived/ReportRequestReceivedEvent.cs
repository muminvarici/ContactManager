using MediatR;

namespace Reports.Worker.Events.ReportRequestReceived;

public class ReportRequestReceivedEvent : INotification
{
    public string Id { get; set; }
}
