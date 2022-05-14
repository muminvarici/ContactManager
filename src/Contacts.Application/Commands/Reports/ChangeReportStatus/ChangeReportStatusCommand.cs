using Contacts.Domain.Entities.Reports;
using MediatR;

namespace Contacts.Application.Commands.Reports.ChangeReportStatus;

public class ChangeReportStatusCommand : IRequest<ChangeReportStatusCommandResult>
{
    public string Id { get; set; }
    public ReportStatus Status { get; set; }
    public string Path { get; set; }
}
