using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Branches.Commands.UpdateBranch;

public class UpdateBranchHandler : IRequestHandler<UpdateBranchCommand, BranchDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBranchHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BranchDto> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = await _unitOfWork.Repository<Branch>().GetByIdAsync(request.Id, cancellationToken);
        if (branch is null)
            throw new KeyNotFoundException($"Branch with id {request.Id} not found");

        branch.Name = request.Name;
        branch.Code = request.Code;
        branch.Address = request.Address;
        branch.Phone = request.Phone;
        branch.IsActive = request.IsActive;

        _unitOfWork.Repository<Branch>().Update(branch);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new BranchDto
        {
            Id = branch.Id,
            Name = branch.Name,
            Code = branch.Code,
            Address = branch.Address,
            Phone = branch.Phone,
            IsActive = branch.IsActive
        };
    }
}
