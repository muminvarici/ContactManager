using Contacts.Domain.Entities.Contacts;
using MediatR;

namespace Contacts.Application.Commands.Contacts.CreateContact;

public class CreateContactCommand : IRequest<CreateContactCommandResult>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Firm { get; set; }
    public List<AdditionalInfo> AdditionalInfo { get; set; }
}
