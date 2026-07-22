using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Branches.Queries.GetBranchById;

public class GetBranchByIdHandler : IRequestHandler<GetBranchByIdQuery, BranchDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetBranchByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BranchDto?> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
    {
        var branch = await _unitOfWork.Repository<Branch>().GetByIdAsync(request.Id, cancellationToken);
        if (branch is null) return null;

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
