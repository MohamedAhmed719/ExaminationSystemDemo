using ExaminationSystemDemo.Abstractions.Consts;
using FluentValidation;

namespace ExaminationSystemDemo.Contracts.Authentiaction;

public class StudentRegisterRequestValidator : AbstractValidator<StudentRegisterRequest>
{
    public StudentRegisterRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.FirstName).NotEmpty().Length(3, 100);
        RuleFor(x => x.LastName).NotEmpty().Length(3, 100);

        RuleFor(x => x.Grade).NotEmpty().Must(x=> x > 0)
            .WithMessage("Grdae value can't be negative");

        RuleFor(x=>x.Password)
            .NotEmpty()
            .Matches(RegexPattern.Password)
            .WithMessage("Password should be at least 8 digits and should contains Lowercase, NonAlphanumeric and Uppercase");
    }
}
