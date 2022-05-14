using Contacts.Domain.Entities.Reports;
using MediatR;

namespace Contacts.Application.Queries.Reports.GetReports;

public class ListAllReportsQueryResult : IRequest<ListAllReportsQueryResult>
{
    public bool IsSuccess { get; set; }
    public List<Report> Data { get; set; }
}
