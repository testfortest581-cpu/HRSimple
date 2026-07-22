using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Leaves.Queries.GetAllLeaves;

public class GetAllLeavesHandler : IRequestHandler<GetAllLeavesQuery, List<LeaveDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllLeavesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<LeaveDto>> Handle(GetAllLeavesQuery request, CancellationToken cancellationToken)
    {
        var leaves = await _unitOfWork.Repository<Leave>().GetAllAsync(cancellationToken);
        var employees = (await _unitOfWork.Repository<Employee>().GetAllAsync(cancellationToken)).ToDictionary(e => e.Id);

        return leaves.Select(l => new LeaveDto
        {
            Id = l.Id,
            EmployeeId = l.EmployeeId,
            EmployeeName = employees.TryGetValue(l.EmployeeId, out var emp) ? emp.FirstName + " " + emp.LastName : string.Empty,
            StartDate = l.StartDate,
            EndDate = l.EndDate,
            LeaveType = l.LeaveType,
            Reason = l.Reason,
            Status = l.Status,
            ApprovedById = l.ApprovedById
        }).ToList();
    }
}
