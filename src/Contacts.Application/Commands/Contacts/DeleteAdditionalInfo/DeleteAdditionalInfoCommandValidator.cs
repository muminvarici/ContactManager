using FluentValidation;

namespace Contacts.Application.Commands.Contacts.DeleteAdditionalInfo;

public class DeleteAdditionalInfoCommandValidator : AbstractValidator<DeleteAdditionalInfoCommand>
{
    public DeleteAdditionalInfoCommandValidator()
    {
        RuleFor(w => w.Id).NotEmpty();
        RuleFor(w => w.InfoId).NotEmpty();
    }
}
