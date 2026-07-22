using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Branches.Queries.GetAllBranches;

public record GetAllBranchesQuery : IRequest<List<BranchDto>>;
