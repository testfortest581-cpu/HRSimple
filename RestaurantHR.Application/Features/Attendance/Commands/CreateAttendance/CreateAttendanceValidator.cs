using FluentValidation;

namespace RestaurantHR.Application.Features.Attendance.Commands.CreateAttendance;

public class CreateAttendanceValidator : AbstractValidator<CreateAttendanceCommand>
{
    public CreateAttendanceValidator()
    {
        RuleFor(r => r.EmployeeId)
            .NotEmpty().WithMessage("Employee is required");

        RuleFor(r => r.Date)
            .NotEmpty().WithMessage("Date is required")
            .Must(d => d != default).WithMessage("Date is required");
    }
}
