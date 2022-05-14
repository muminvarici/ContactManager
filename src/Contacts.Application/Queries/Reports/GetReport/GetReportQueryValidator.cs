using FluentValidation;
using MongoDB.Bson;

namespace Contacts.Application.Queries.Reports.GetReport;

public class GetReportQueryValidator : AbstractValidator<GetReportQuery>
{
    public GetReportQueryValidator()
    {
        RuleFor(w => w.Id)
            .NotEmpty()
            .Must(id => ObjectId.TryParse(id, out _)).WithMessage("Id must be ObjectId");
    }
}
