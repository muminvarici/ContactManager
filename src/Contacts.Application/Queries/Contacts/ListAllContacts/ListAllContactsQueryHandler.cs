using Contacts.Domain.Entities.Contacts;
using Contacts.Domain.Repositories.Abstracts;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Contacts.Application.Queries.Contacts;

public class ListAllContactsQueryHandler : IRequestHandler<ListAllContactsQuery, ListAllContactsQueryResult>
{
    private readonly IGenericRepository<Contact> _repository;
    private readonly ILogger<ListAllContactsQueryHandler> _logger;

    public ListAllContactsQueryHandler(
        IGenericRepository<Contact> repository,
        ILogger<ListAllContactsQueryHandler> logger
    )
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ListAllContactsQueryResult> Handle(ListAllContactsQuery request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAllAsync();

        _logger.LogInformation($"Listed all contacts count:{data?.Count}");

        return new ListAllContactsQueryResult
        {
            IsSuccess = data != null, Data = data?.Adapt<List<ListAllContactsQueryResultItem>>()
        };
    }
}
