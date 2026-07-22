using MediatR;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Enums;

namespace RestaurantHR.Application.Features.Leaves.Commands.CreateLeave;

public record CreateLeaveCommand : IRequest<LeaveDto>
{
    public Guid EmployeeId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public LeaveType LeaveType { get; init; }
    public string Reason { get; init; } = string.Empty;
}
