using MediatR;

namespace Contacts.Application.Commands.Contacts.DeleteContact;

public class DeleteContactCommand : IRequest<DeleteContactCommandResult>
{
    public string Id { get; set; }
}
