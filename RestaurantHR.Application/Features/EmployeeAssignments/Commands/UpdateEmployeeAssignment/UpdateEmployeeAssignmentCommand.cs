using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.EmployeeAssignments.Commands.UpdateEmployeeAssignment;

public record UpdateEmployeeAssignmentCommand : IRequest<EmployeeAssignmentDto>
{
    public Guid Id { get; init; }
    public Guid EmployeeId { get; init; }
    public Guid ShiftId { get; init; }
    public Guid BranchId { get; init; }
    public string WorkDays { get; init; } = "[]";
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public bool IsActive { get; init; }
}
