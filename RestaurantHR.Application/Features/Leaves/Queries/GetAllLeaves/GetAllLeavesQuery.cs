using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Leaves.Queries.GetAllLeaves;

public record GetAllLeavesQuery : IRequest<List<LeaveDto>>;
