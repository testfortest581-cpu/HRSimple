using FluentValidation;

namespace RestaurantHR.Application.Features.Payments.Commands.CreatePayment;

public class CreatePaymentValidator : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentValidator()
    {
        RuleFor(r => r.EmployeeId)
            .NotEmpty().WithMessage("Employee is required");

        RuleFor(r => r.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero");
    }
}
