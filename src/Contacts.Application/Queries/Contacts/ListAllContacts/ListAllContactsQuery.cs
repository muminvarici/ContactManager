using MediatR;

namespace Contacts.Application.Queries.Contacts;

public class ListAllContactsQuery : IRequest<ListAllContactsQueryResult>
{
}
