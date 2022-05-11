using MediatR;

namespace Contacts.Application.Commands.Contacts.DeleteContact;

public class DeleteContactCommandResult : IRequest<DeleteContactCommandResult>
{
    public bool IsSuccess { get; set; }
}