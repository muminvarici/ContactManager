using Contacts.Application.Events.Reports;
using MediatR;

namespace Contacts.Application.Commands.Reports.CreateReport;

public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, CreateReportCommandResult>
{
    private readonly IPublisher _publisher;

    public CreateReportCommandHandler(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task<CreateReportCommandResult> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        await _publisher.Publish(new ReportRequestReceivedEvent(), cancellationToken);
        return new CreateReportCommandResult
        {
            IsSuccess = true
        };
    }
}
