using FluentValidation;

namespace ExaminationSystemDemo.Contracts.Courses;

public class CourseRequestValidator : AbstractValidator<CourseRequest>
{
    public CourseRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(3,100);
        RuleFor(x => x.Hours).NotEmpty()
            .Must(x=>x>0);
        RuleFor(x => x.Degree).NotEmpty();
    }
}
