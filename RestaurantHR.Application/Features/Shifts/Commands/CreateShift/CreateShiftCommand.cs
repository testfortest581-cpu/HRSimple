using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Shifts.Commands.CreateShift;

public record CreateShiftCommand : IRequest<ShiftDto>
{
    public string Name { get; init; } = string.Empty;
    public TimeSpan StartTime { get; init; }
    public TimeSpan EndTime { get; init; }
    public Guid BranchId { get; init; }
}
