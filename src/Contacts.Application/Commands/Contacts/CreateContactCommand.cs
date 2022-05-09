using Contacts.Domain.Entities.Contacts;
using MediatR;

namespace Contacts.Application.Commands.Contacts;

public class CreateContactCommand : IRequest<CreateContactCommandResult>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Firm { get; set; }
    public AdditionalInfo AdditionalInfo { get; set; }
}
