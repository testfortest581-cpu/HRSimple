using AutoMapper;
using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Branches.Commands.CreateBranch;

public class CreateBranchHandler : IRequestHandler<CreateBranchCommand, BranchDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateBranchHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BranchDto> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = new Branch
        {
            Name = request.Name,
            Code = request.Code,
            Address = request.Address,
            Phone = request.Phone
        };

        await _unitOfWork.Repository<Branch>().AddAsync(branch, cancellationToken);
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
