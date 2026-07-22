using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.My.Commands.CreateMyLeave;

public record CreateMyLeaveCommand : IRequest<MyLeaveDto>
{
    public Guid EmployeeId { get; init; }
    public string StartDate { get; init; } = string.Empty;
    public string EndDate { get; init; } = string.Empty;
    public string LeaveType { get; init; } = "Annual";
    public string Reason { get; init; } = string.Empty;
}