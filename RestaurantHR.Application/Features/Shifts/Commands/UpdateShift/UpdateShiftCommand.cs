using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Shifts.Commands.UpdateShift;

public record UpdateShiftCommand : IRequest<ShiftDto>
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public TimeSpan StartTime { get; init; }
    public TimeSpan EndTime { get; init; }
    public Guid BranchId { get; init; }
    public bool? IsActive { get; init; }
}
