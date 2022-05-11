using Contacts.Domain.Entities.Contacts;
using MediatR;

namespace Contacts.Application.Commands.Contacts.CreateAdditionalInfo;

public class CreateAdditionalInfoCommand : IRequest<CreateAdditionalInfoCommandResult>
{
    public string Id { get; set; }
    public AdditionalInfo AdditionalInfo { get; set; }
}
