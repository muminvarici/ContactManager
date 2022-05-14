using Contacts.Application.Events.Reports;
using Contacts.Domain.Entities.Reports;
using Contacts.Domain.Repositories.Abstracts;
using MediatR;

namespace Contacts.Application.Commands.Reports.CreateReport;

public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, CreateReportCommandResult>
{
    private readonly IPublisher _publisher;
    private readonly IGenericRepository<Report> _repository;

    public CreateReportCommandHandler(
        IPublisher publisher,
        IGenericRepository<Report> repository
    )
    {
        _publisher = publisher;
        _repository = repository;
    }

    public async Task<CreateReportCommandResult> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        var report = new Report
        {
            Status = ReportStatus.Preparing
        };
        await _repository.CreateAsync(report);
        await _publisher.Publish(new ReportRequestReceivedEvent
        {
            Id = report.Id
        }, cancellationToken);
        return new CreateReportCommandResult
        {
            IsSuccess = true
        };
    }
}
