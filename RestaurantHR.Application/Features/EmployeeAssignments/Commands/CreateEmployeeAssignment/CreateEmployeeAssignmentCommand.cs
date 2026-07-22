using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.EmployeeAssignments.Commands.CreateEmployeeAssignment;

public record CreateEmployeeAssignmentCommand : IRequest<EmployeeAssignmentDto>
{
    public Guid EmployeeId { get; init; }
    public Guid ShiftId { get; init; }
    public Guid BranchId { get; init; }
    public string WorkDays { get; init; } = "[]";
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}
