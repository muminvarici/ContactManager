using MediatR;

namespace Contacts.Application.Queries.Contacts.GetContact;

public class GetContactQuery : IRequest<GetContactQueryResult>
{
    public string Id { get; set; }
}
