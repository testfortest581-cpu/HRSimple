using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.WeeklyAssignments.Commands.CreateWeeklyAssignment;

public record CreateWeeklyAssignmentCommand : IRequest<WeeklyAssignmentDto>
{
    public Guid EmployeeId { get; init; }
    public Guid BranchId { get; init; }
    public Guid ShiftId { get; init; }
    public string WorkDays { get; init; } = "[]";
    public DateTime WeekStartDate { get; init; }
}
