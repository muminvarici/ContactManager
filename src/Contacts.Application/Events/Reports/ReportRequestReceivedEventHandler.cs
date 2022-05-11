using Contacts.Domain.Events.Abstractions;
using MediatR;

namespace Contacts.Application.Events.Reports;

public class ReportRequestReceivedEventHandler : INotificationHandler<ReportRequestReceivedEvent>
{
    private readonly IEventPublisher _eventPublisher;

    public ReportRequestReceivedEventHandler(IEventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
    }

    //this request is handled by notificiation because something can be implemented in the future
    public Task Handle(ReportRequestReceivedEvent notification, CancellationToken cancellationToken)
    {
        _ = _eventPublisher.Enqueue("report", notification);
        return Task.CompletedTask;
    }
}
