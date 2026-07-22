using MediatR;

namespace RestaurantHR.Application.Features.Shifts.Commands.DeleteShift;

public record DeleteShiftCommand(Guid Id) : IRequest;
