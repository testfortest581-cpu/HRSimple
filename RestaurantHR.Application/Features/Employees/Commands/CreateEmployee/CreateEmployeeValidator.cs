using FluentValidation;

namespace RestaurantHR.Application.Features.Employees.Commands.CreateEmployee;

public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeValidator()
    {
        RuleFor(r => r.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50);

        RuleFor(r => r.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50);

        RuleFor(r => r.NationalCode)
            .NotEmpty().WithMessage("National code is required")
            .MaximumLength(20);

        RuleFor(r => r.BaseSalary)
            .GreaterThanOrEqualTo(0).WithMessage("Base salary must be non-negative");
    }
}
