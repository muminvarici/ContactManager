using MediatR;

namespace Contacts.Application.Commands.Reports.ChangeReportStatus;

public class ChangeReportStatusCommandResult : IRequest<ChangeReportStatusCommandResult>
{
    public bool IsSuccess { get; set; }
}