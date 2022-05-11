using Contacts.Domain.Entities.Contacts;
using Contacts.Domain.Repositories.Abstracts;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace Contacts.Application.Commands.Contacts.CreateContact;

public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, CreateContactCommandResult>
{
    private readonly IGenericRepository<Contact> _repository;
    private readonly ILogger<CreateContactCommandHandler> _logger;

    public CreateContactCommandHandler(
        IGenericRepository<Contact> repository,
        ILogger<CreateContactCommandHandler> logger
    )
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<CreateContactCommandResult> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<Contact>();
        entity.AdditionalInfo?.ForEach(w => w.Id = ObjectId.GenerateNewId().ToString());
        _ = await _repository.CreateAsync(entity);

        _logger.LogInformation($"New record created with id {entity.Id}");

        return new CreateContactCommandResult
        {
            IsSuccess = !string.IsNullOrWhiteSpace(entity.Id)
        };
    }
}
