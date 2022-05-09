using FluentValidation;
using MediatR;

namespace Contacts.Api.PipelineBehaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators
            .Select(w => w.Validate(context))
            .SelectMany(w => w.Errors)
            .Where(w => w != null)
            .ToList();
        return failures.Any() ? throw new ValidationException(failures) : next();
    }
}
