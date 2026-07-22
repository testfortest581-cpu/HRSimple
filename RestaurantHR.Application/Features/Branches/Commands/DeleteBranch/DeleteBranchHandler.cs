using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Branches.Commands.DeleteBranch;

public class DeleteBranchHandler : IRequestHandler<DeleteBranchCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBranchHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = await _unitOfWork.Repository<Branch>().GetByIdAsync(request.Id, cancellationToken);
        if (branch is null)
            throw new KeyNotFoundException($"Branch with id {request.Id} not found");

        branch.IsActive = false;
        _unitOfWork.Repository<Branch>().Update(branch);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
