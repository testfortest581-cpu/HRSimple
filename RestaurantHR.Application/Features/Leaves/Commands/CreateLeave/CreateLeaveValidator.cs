using FluentValidation;

namespace RestaurantHR.Application.Features.Leaves.Commands.CreateLeave;

public class CreateLeaveValidator : AbstractValidator<CreateLeaveCommand>
{
    public CreateLeaveValidator()
    {
        RuleFor(r => r.EmployeeId)
            .NotEmpty().WithMessage("Employee is required");

        RuleFor(r => r.StartDate)
            .NotEmpty().WithMessage("Start date is required");

        RuleFor(r => r.EndDate)
            .NotEmpty().WithMessage("End date is required")
            .GreaterThanOrEqualTo(r => r.StartDate).WithMessage("End date must be after or equal to start date");

        RuleFor(r => r.LeaveType)
            .IsInEnum().WithMessage("Invalid leave type");

        RuleFor(r => r.Reason)
            .NotEmpty().WithMessage("Reason is required")
            .MaximumLength(500);
    }
}
