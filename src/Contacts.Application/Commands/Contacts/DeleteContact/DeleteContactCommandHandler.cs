using Contacts.Domain.Entities.Contacts;
using Contacts.Domain.Repositories.Abstracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Contacts.Application.Commands.Contacts.DeleteContact;

public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, DeleteContactCommandResult>
{
    private readonly IGenericRepository<Contact> _repository;
    private readonly ILogger<DeleteContactCommandHandler> _logger;

    public DeleteContactCommandHandler(
        IGenericRepository<Contact> repository,
        ILogger<DeleteContactCommandHandler> logger
    )
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<DeleteContactCommandResult> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        var result = new DeleteContactCommandResult
        {
            IsSuccess = await _repository.RemoveAsync(request.Id)
        };

        _logger.LogInformation($"Deleted contact id {request.Id} result {result.IsSuccess}");

        return result;
    }
}
