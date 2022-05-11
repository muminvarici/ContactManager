using MediatR;

namespace Contacts.Application.Commands.Reports.CreateReport;

public class CreateReportCommandResult : IRequest<CreateReportCommandResult>
{
    public bool IsSuccess { get; set; }
}