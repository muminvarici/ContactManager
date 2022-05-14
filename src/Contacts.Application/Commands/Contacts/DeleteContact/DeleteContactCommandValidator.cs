using FluentValidation;
using MongoDB.Bson;

namespace Contacts.Application.Commands.Contacts.DeleteContact;

public class DeleteContactCommandValidator : AbstractValidator<DeleteContactCommand>
{
    public DeleteContactCommandValidator()
    {
        RuleFor(w => w.Id)
            .NotEmpty()
            .Must(id => ObjectId.TryParse(id, out _)).WithMessage("Id must be ObjectId");    }
}
