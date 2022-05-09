using FluentValidation;

namespace Contacts.Application.Commands.Contacts;

public class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
{
    public CreateContactCommandValidator()
    {
    }
}