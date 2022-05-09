using Contacts.Domain.Entities.Contacts;
using Contacts.Domain.Repositories.Abstracts;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Contacts.Application.Commands.Contacts;

public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, CreateContactCommandResult>
{
    private readonly IGenericRepository<Contact> _repository;
    private readonly ILogger<CreateContactCommandHandler> _logger;

    public CreateContactCommandHandler(
        IGenericRepository<Contact> repository,
    )
    {
        _repository = repository;
    }

    public async Task<CreateContactCommandResult> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<Contact>();
        _ = await _repository.CreateAsync(entity);

        return new CreateContactCommandResult
        {
            IsSuccess = true
        };
    }
}
