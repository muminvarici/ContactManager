using MediatR;

namespace Contacts.Application.Commands.Contacts.DeleteAdditionalInfo;

public class DeleteAdditionalInfoCommand : IRequest<DeleteAdditionalInfoCommandResult>
{
    public string Id { get; set; }
    public string InfoId { get; set; }
}
