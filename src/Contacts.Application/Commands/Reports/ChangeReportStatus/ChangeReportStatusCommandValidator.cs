using FluentValidation;
using MongoDB.Bson;

namespace Contacts.Application.Commands.Reports.ChangeReportStatus;

public class ChangeReportStatusCommandValidator : AbstractValidator<ChangeReportStatusCommand>
{
    public ChangeReportStatusCommandValidator()
    {
        RuleFor(w => w.Id)
            .NotEmpty()
            .Must(id => ObjectId.TryParse(id, out _)).WithMessage("Id must be ObjectId");

        RuleFor(w => w.Path)
            .NotEmpty()
            .Must(Path.IsPathFullyQualified)//normally, it's not required.
            .WithMessage("File path is not valid");

        RuleFor(w => w.Status)
            .IsInEnum();
    }
}
