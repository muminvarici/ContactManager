using Contacts.Domain.Entities.Contacts;
using MediatR;

namespace Contacts.Application.Queries.Contacts.ListAllContacts;

public class ListAllContactsQueryResult : IRequest<ListAllContactsQueryResult>
{
    public bool IsSuccess { get; set; }
    public IEnumerable<Contact> Data { get; set; }
}
