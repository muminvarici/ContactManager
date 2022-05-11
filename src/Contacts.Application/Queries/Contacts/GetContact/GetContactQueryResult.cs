using Contacts.Domain.Entities.Contacts;
using MediatR;

namespace Contacts.Application.Queries.Contacts.GetContact;

public class GetContactQueryResult : IRequest<GetContactQueryResult>
{
    public bool IsSuccess { get; set; }
    public Contact Data { get; set; }
}
