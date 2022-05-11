using FluentValidation;

namespace Contacts.Application.Commands.Contacts.CreateAdditionalInfo;

public class CreateAdditionalInfoCommandValidator : AbstractValidator<CreateAdditionalInfoCommand>
{
    public CreateAdditionalInfoCommandValidator()
    {
        RuleFor(w => w.Id).NotEmpty();
        RuleFor(w => w.AdditionalInfo).NotEmpty();
        RuleFor(w => w.AdditionalInfo.Type).IsInEnum();
        RuleFor(w => w.AdditionalInfo.Value).NotEmpty();
        RuleFor(w => w.AdditionalInfo.Id).Empty();
    }
}
