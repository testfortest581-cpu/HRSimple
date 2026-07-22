using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Leaves.Queries.GetLeaveById;

public class GetLeaveByIdHandler : IRequestHandler<GetLeaveByIdQuery, LeaveDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetLeaveByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<LeaveDto?> Handle(GetLeaveByIdQuery request, CancellationToken cancellationToken)
    {
        var leave = await _unitOfWork.Repository<Leave>().GetByIdAsync(request.Id, cancellationToken);
        if (leave is null) return null;

        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(leave.EmployeeId, cancellationToken);

        return new LeaveDto
        {
            Id = leave.Id,
            EmployeeId = leave.EmployeeId,
            EmployeeName = employee is not null ? employee.FirstName + " " + employee.LastName : string.Empty,
            StartDate = leave.StartDate,
            EndDate = leave.EndDate,
            LeaveType = leave.LeaveType,
            Reason = leave.Reason,
            Status = leave.Status,
            ApprovedById = leave.ApprovedById
        };
    }
}
