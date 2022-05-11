using FluentValidation;

namespace Contacts.Application.Commands.Contacts.DeleteContact;

public class DeleteContactCommandValidator : AbstractValidator<DeleteContactCommand>
{
    public DeleteContactCommandValidator()
    {
        RuleFor(w => w.Id).NotEmpty();
    }
}
