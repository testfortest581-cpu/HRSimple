using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Branches.Commands.UpdateBranch;

public record UpdateBranchCommand : IRequest<BranchDto>
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public bool IsActive { get; init; }
}
