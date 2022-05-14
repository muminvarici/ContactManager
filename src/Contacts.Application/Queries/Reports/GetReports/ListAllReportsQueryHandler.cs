using Contacts.Domain.Entities.Reports;
using Contacts.Domain.Repositories.Abstracts;
using MediatR;

namespace Contacts.Application.Queries.Reports.GetReports;

public class ListAllReportsQueryHandler : IRequestHandler<ListAllReportsQuery, ListAllReportsQueryResult>
{
    private readonly IGenericRepository<Report> _repository;

    public ListAllReportsQueryHandler(
        IGenericRepository<Report> repository
    )
    {
        _repository = repository;
    }

    public async Task<ListAllReportsQueryResult> Handle(ListAllReportsQuery request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAllAsync();
        return new ListAllReportsQueryResult
        {
            IsSuccess = data != null, Data = data
        };
    }
}
