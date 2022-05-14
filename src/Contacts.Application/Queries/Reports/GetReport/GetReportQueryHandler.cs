using Contacts.Domain.Entities.Reports;
using Contacts.Domain.Repositories.Abstracts;
using MediatR;

namespace Contacts.Application.Queries.Reports.GetReport;

public class GetReportQueryHandler : IRequestHandler<GetReportQuery, GetReportQueryResult>
{
    private readonly IGenericRepository<Report> _repository;

    public GetReportQueryHandler(
        IGenericRepository<Report> repository
    )
    {
        _repository = repository;
    }

    public async Task<GetReportQueryResult> Handle(GetReportQuery request, CancellationToken cancellationToken)
    {
        var data = new Report()
        {
            Path = "/Users/mumin.varici/Downloads/Template.xlsx"
        };
        // var data = await _repository.GetOneAsync(request.Id);
        if (data == null)
            return new GetReportQueryResult();

        var bytes = await File.ReadAllBytesAsync(data.Path, cancellationToken);

        return new GetReportQueryResult
        {
            IsSuccess = true, Data = bytes, FileName = Path.GetFileName(data.Path)
        };
    }
}
