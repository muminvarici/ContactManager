using MediatR;

namespace Contacts.Application.Commands.Contacts.CreateAdditionalInfo;

public class CreateAdditionalInfoCommandResult : IRequest<CreateAdditionalInfoCommandResult>
{
    public bool IsSuccess { get; set; }
}