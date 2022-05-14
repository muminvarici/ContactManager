using MediatR;

namespace Contacts.Application.Queries.Contacts.ListAllContacts;

public class ListAllContactsQuery : IRequest<ListAllContactsQueryResult>
{
    public bool WithDetails { get; set; }
}
