using FluentValidation;

namespace Contacts.Application.Queries.Contacts.GetContact;

public class GetContactQueryValidator : AbstractValidator<GetContactQuery>
{
    public GetContactQueryValidator()
    {
        RuleFor(w => w.Id).NotEmpty();
    }
}
