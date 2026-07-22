using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Branches.Commands.CreateBranch;

public record CreateBranchCommand : IRequest<BranchDto>
{
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
}
