using Contacts.Domain.Entities.Contacts;
using Contacts.Domain.Repositories.Abstracts;
using MediatR;

namespace Contacts.Application.Queries.Contacts.GetContact;

public class GetContactQueryHandler : IRequestHandler<GetContactQuery, GetContactQueryResult>
{
    private readonly IGenericRepository<Contact> _repository;

    public GetContactQueryHandler(
        IGenericRepository<Contact> repository
    )
    {
        _repository = repository;
    }

    public async Task<GetContactQueryResult> Handle(GetContactQuery request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetOneAsync(request.Id);

        return new GetContactQueryResult
        {
            IsSuccess = data != null, Data = data
        };
    }
}
