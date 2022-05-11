using MediatR;

namespace Contacts.Application.Commands.Reports.CreateReport;

public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, CreateReportCommandResult>
{
    public CreateReportCommandHandler() { }

    public async Task<CreateReportCommandResult> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        return new CreateReportCommandResult
        {
            IsSuccess = true
        };
    }
}
