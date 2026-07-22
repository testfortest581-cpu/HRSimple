using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.My.Queries.GetMyLeaves;

public record GetMyLeavesQuery(Guid EmployeeId) : IRequest<List<MyLeaveDto>>;