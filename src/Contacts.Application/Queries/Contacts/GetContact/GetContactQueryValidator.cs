using FluentValidation;
using MongoDB.Bson;

namespace Contacts.Application.Queries.Contacts.GetContact;

public class GetContactQueryValidator : AbstractValidator<GetContactQuery>
{
    public GetContactQueryValidator()
    {
        RuleFor(w => w.Id)
            .NotEmpty()
            .Must(id => ObjectId.TryParse(id, out _)).WithMessage("Id must be ObjectId");    }
}
