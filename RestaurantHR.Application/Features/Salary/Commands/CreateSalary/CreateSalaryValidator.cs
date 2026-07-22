using FluentValidation;

namespace RestaurantHR.Application.Features.Salary.Commands.CreateSalary;

public class CreateSalaryValidator : AbstractValidator<CreateSalaryCommand>
{
    public CreateSalaryValidator()
    {
        RuleFor(r => r.EmployeeId)
            .NotEmpty().WithMessage("Employee is required");

        RuleFor(r => r.Month)
            .InclusiveBetween(1, 12).WithMessage("Month must be between 1 and 12");

        RuleFor(r => r.Year)
            .GreaterThanOrEqualTo(2020).WithMessage("Year must be 2020 or later");
    }
}
