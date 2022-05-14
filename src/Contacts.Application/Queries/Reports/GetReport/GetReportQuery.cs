using MediatR;

namespace Contacts.Application.Queries.Reports.GetReport;

public class GetReportQuery : IRequest<GetReportQueryResult>
{
    public string Id { get; set; }
}
