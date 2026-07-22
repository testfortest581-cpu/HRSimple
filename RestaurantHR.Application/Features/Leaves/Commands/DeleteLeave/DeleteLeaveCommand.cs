using MediatR;

namespace RestaurantHR.Application.Features.Leaves.Commands.DeleteLeave;

public record DeleteLeaveCommand(Guid Id) : IRequest;
