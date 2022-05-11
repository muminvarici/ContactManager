using Contacts.Application.Commands.Contacts.CreateContact;
using Contacts.Domain.Entities.Contacts;
using Contacts.Domain.Repositories.Abstracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Contacts.Application.Commands.Contacts.DeleteAdditionalInfo;

public class DeleteAdditionalInfoCommandHandler : IRequestHandler<DeleteAdditionalInfoCommand, DeleteAdditionalInfoCommandResult>
{
    private readonly IGenericRepository<Contact> _repository;
    private readonly ILogger<CreateContactCommandHandler> _logger;

    public DeleteAdditionalInfoCommandHandler(
        IGenericRepository<Contact> repository,
        ILogger<CreateContactCommandHandler> logger
    )
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<DeleteAdditionalInfoCommandResult> Handle(DeleteAdditionalInfoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetOneAsync(request.Id);

        entity.AdditionalInfo ??= new List<AdditionalInfo>();
        entity.AdditionalInfo.RemoveAll(w => w.Id == request.InfoId);

        var result = new DeleteAdditionalInfoCommandResult
        {
            IsSuccess = await _repository.UpdateAsync(entity)
        };

        _logger.LogInformation($"Additional info deleted for contact id {request.Id} info id {request.InfoId} result {result.IsSuccess}");
        return result;
    }
}
