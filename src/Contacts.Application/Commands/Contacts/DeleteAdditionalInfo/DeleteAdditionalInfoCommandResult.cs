using MediatR;

namespace Contacts.Application.Commands.Contacts.DeleteAdditionalInfo;

public class DeleteAdditionalInfoCommandResult : IRequest<DeleteAdditionalInfoCommandResult>
{
    public bool IsSuccess { get; set; }
}