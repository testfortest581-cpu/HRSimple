using FluentValidation;

namespace RestaurantHR.Application.Features.WeeklyAssignments.Commands.CreateWeeklyAssignment;

public class CreateWeeklyAssignmentValidator : AbstractValidator<CreateWeeklyAssignmentCommand>
{
    public CreateWeeklyAssignmentValidator()
    {
        RuleFor(r => r.EmployeeId).NotEmpty().WithMessage("Employee is required");
        RuleFor(r => r.BranchId).NotEmpty().WithMessage("Branch is required");
        RuleFor(r => r.ShiftId).NotEmpty().WithMessage("Shift is required");
    }
}
