using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Branches.Queries.GetAllBranches;

public class GetAllBranchesHandler : IRequestHandler<GetAllBranchesQuery, List<BranchDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllBranchesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<BranchDto>> Handle(GetAllBranchesQuery request, CancellationToken cancellationToken)
    {
        var branches = await _unitOfWork.Repository<Branch>().FindAsync(b => b.IsActive, cancellationToken);
        return branches.Select(b => new BranchDto
        {
            Id = b.Id,
            Name = b.Name,
            Code = b.Code,
            Address = b.Address,
            Phone = b.Phone,
            IsActive = b.IsActive
        }).ToList();
    }
}
