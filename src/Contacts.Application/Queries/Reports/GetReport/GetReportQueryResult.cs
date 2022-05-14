using MediatR;

namespace Contacts.Application.Queries.Reports.GetReport;

public class GetReportQueryResult : IRequest<GetReportQueryResult>
{
    public bool IsSuccess { get; set; }
    public Memory<byte> Data { get; set; }
    public string FileName { get; set; }
}
