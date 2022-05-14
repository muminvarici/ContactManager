using FluentValidation;
using MongoDB.Bson;

namespace Contacts.Application.Commands.Contacts.DeleteAdditionalInfo;

public class DeleteAdditionalInfoCommandValidator : AbstractValidator<DeleteAdditionalInfoCommand>
{
    public DeleteAdditionalInfoCommandValidator()
    {
        RuleFor(w => w.Id)
            .NotEmpty()
            .Must(id => ObjectId.TryParse(id, out _)).WithMessage("Id must be ObjectId");
        RuleFor(w => w.InfoId)
            .NotEmpty()
            .Must(id => ObjectId.TryParse(id, out _)).WithMessage("InfoId must be ObjectId");
    }
}
