using MediatR;

namespace Contacts.Application.Commands.Contacts;

public class CreateContactCommandResult : IRequest<CreateContactCommandResult>
{
    public bool IsSuccess { get; set; }
}