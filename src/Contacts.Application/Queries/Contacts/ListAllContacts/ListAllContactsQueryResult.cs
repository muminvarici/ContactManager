using MediatR;

namespace Contacts.Application.Queries.Contacts;

public class ListAllContactsQueryResult : IRequest<ListAllContactsQueryResult>
{
    public bool IsSuccess { get; set; }
    public List<ListAllContactsQueryResultItem> Data { get; set; }
}

public class ListAllContactsQueryResultItem
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Firm { get; set; }
}
