using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Branches.Queries.GetBranchById;

public record GetBranchByIdQuery(Guid Id) : IRequest<BranchDto?>;
