using MediatR;

namespace RestaurantHR.Application.Features.Branches.Commands.DeleteBranch;

public record DeleteBranchCommand(Guid Id) : IRequest;
