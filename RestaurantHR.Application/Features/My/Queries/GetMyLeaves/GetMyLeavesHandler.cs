using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;
using RestaurantHR.Domain.Enums;

namespace RestaurantHR.Application.Features.My.Queries.GetMyLeaves;

public class GetMyLeavesHandler : IRequestHandler<GetMyLeavesQuery, List<MyLeaveDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMyLeavesHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<List<MyLeaveDto>> Handle(GetMyLeavesQuery request, CancellationToken cancellationToken)
    {
        var leaves = await _unitOfWork.Repository<Leave>().FindAsync(l => l.EmployeeId == request.EmployeeId, cancellationToken);
        return leaves.OrderByDescending(l => l.CreatedAt).Select(l => new MyLeaveDto
        {
            Id = l.Id,
            StartDate = l.StartDate.ToString("yyyy-MM-dd"),
            EndDate = l.EndDate.ToString("yyyy-MM-dd"),
            LeaveType = l.LeaveType.ToString(),
            Reason = l.Reason,
            Status = l.Status.ToString(),
        }).ToList();
    }
}