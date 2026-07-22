using FluentValidation;

namespace RestaurantHR.Application.Features.EmployeeAssignments.Commands.CreateEmployeeAssignment;

public class CreateEmployeeAssignmentValidator : AbstractValidator<CreateEmployeeAssignmentCommand>
{
    public CreateEmployeeAssignmentValidator()
    {
        RuleFor(r => r.EmployeeId)
            .NotEmpty().WithMessage("Employee is required");

        RuleFor(r => r.ShiftId)
            .NotEmpty().WithMessage("Shift is required");

        RuleFor(r => r.BranchId)
            .NotEmpty().WithMessage("Branch is required");
    }
}
