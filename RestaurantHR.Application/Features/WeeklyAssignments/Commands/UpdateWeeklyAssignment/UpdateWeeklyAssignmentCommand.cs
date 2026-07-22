using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.WeeklyAssignments.Commands.UpdateWeeklyAssignment;

public record UpdateWeeklyAssignmentCommand : IRequest<WeeklyAssignmentDto>
{
    public Guid Id { get; init; }
    public Guid EmployeeId { get; init; }
    public Guid BranchId { get; init; }
    public Guid ShiftId { get; init; }
    public string WorkDays { get; init; } = "[]";
    public DateTime WeekStartDate { get; init; }
    public bool? IsActive { get; init; }
}
