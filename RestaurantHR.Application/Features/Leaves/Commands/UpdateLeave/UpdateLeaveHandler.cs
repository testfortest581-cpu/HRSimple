using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Leaves.Commands.UpdateLeave;

public class UpdateLeaveHandler : IRequestHandler<UpdateLeaveCommand, LeaveDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLeaveHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<LeaveDto> Handle(UpdateLeaveCommand request, CancellationToken cancellationToken)
    {
        var leave = await _unitOfWork.Repository<Leave>().GetByIdAsync(request.Id, cancellationToken);
        if (leave is null)
            throw new KeyNotFoundException($"Leave with id {request.Id} not found");

        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.EmployeeId, cancellationToken);

        leave.EmployeeId = request.EmployeeId;
        leave.StartDate = request.StartDate;
        leave.EndDate = request.EndDate;
        leave.LeaveType = request.LeaveType;
        leave.Reason = request.Reason;
        leave.Status = request.Status;
        leave.ApprovedById = request.ApprovedById;

        _unitOfWork.Repository<Leave>().Update(leave);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

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
