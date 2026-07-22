using FluentValidation;

namespace RestaurantHR.Application.Features.Shifts.Commands.CreateShift;

public class CreateShiftValidator : AbstractValidator<CreateShiftCommand>
{
    public CreateShiftValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("Shift name is required")
            .MaximumLength(100);

        RuleFor(r => r.StartTime)
            .NotEmpty().WithMessage("Start time is required");

        RuleFor(r => r.EndTime)
            .NotEmpty().WithMessage("End time is required");

        RuleFor(r => r.BranchId)
            .NotEmpty().WithMessage("Branch is required");
    }
}
