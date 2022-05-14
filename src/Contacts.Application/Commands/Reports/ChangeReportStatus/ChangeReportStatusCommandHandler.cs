using Contacts.Domain.Entities.Reports;
using Contacts.Domain.Repositories.Abstracts;
using MediatR;

namespace Contacts.Application.Commands.Reports.ChangeReportStatus;

public class ChangeReportStatusCommandHandler : IRequestHandler<ChangeReportStatusCommand, ChangeReportStatusCommandResult>
{
    private readonly IPublisher _publisher;
    private readonly IGenericRepository<Report> _repository;

    public ChangeReportStatusCommandHandler(
        IPublisher publisher,
        IGenericRepository<Report> repository
    )
    {
        _publisher = publisher;
        _repository = repository;
    }

    public async Task<ChangeReportStatusCommandResult> Handle(ChangeReportStatusCommand request, CancellationToken cancellationToken)
    {
        var result = new ChangeReportStatusCommandResult();

        var item = await _repository.GetOneAsync(request.Id);
        if (item == null) return result;

        item.Status = request.Status;
        item.Path = request.Path;
        await _repository.UpdateAsync(item);
        result.IsSuccess = true;

        return result;
    }
}
