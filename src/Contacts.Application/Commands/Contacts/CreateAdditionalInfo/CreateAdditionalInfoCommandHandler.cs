using Contacts.Application.Commands.Contacts.CreateContact;
using Contacts.Domain.Entities.Contacts;
using Contacts.Domain.Repositories.Abstracts;
using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace Contacts.Application.Commands.Contacts.CreateAdditionalInfo;

public class CreateAdditionalInfoCommandHandler : IRequestHandler<CreateAdditionalInfoCommand, CreateAdditionalInfoCommandResult>
{
    private readonly IGenericRepository<Contact> _repository;
    private readonly ILogger<CreateContactCommandHandler> _logger;

    public CreateAdditionalInfoCommandHandler(
        IGenericRepository<Contact> repository,
        ILogger<CreateContactCommandHandler> logger
    )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task<CreateAdditionalInfoCommandResult> Handle(CreateAdditionalInfoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetOneAsync(request.Id);

        entity.AdditionalInfo ??= new List<AdditionalInfo>();
        request.AdditionalInfo.Id = ObjectId.GenerateNewId().ToString();
        entity.AdditionalInfo.Add(request.AdditionalInfo);

        var result = new CreateAdditionalInfoCommandResult
        {
            IsSuccess = await _repository.UpdateAsync(entity)
        };

        _logger.LogInformation($"New additional info contact id {request.Id} result {result.IsSuccess}");
        return result;
    }
}
