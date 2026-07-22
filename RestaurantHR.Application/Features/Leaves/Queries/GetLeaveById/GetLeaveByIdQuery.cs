using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Leaves.Queries.GetLeaveById;

public record GetLeaveByIdQuery(Guid Id) : IRequest<LeaveDto?>;
