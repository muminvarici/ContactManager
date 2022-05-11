using FluentValidation;

namespace Contacts.Application.Commands.Contacts.CreateContact;

public class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
{
    public CreateContactCommandValidator()
    {
        RuleFor(w => w.Name).NotEmpty();
        RuleFor(w => w.Surname).NotEmpty();
        RuleFor(w => w.Firm).NotEmpty();
    }
}
