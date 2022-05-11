using MediatR;

namespace Contacts.Application.Commands.Contacts.CreateContact;

public class CreateContactCommandResult : IRequest<CreateContactCommandResult>
{
    public bool IsSuccess { get; set; }
}