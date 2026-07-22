using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Leaves.Commands.CreateLeave;

public class CreateLeaveHandler : IRequestHandler<CreateLeaveCommand, LeaveDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateLeaveHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<LeaveDto> Handle(CreateLeaveCommand request, CancellationToken cancellationToken)
    {
        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.EmployeeId, cancellationToken);

        var leave = new Leave
        {
            EmployeeId = request.EmployeeId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            LeaveType = request.LeaveType,
            Reason = request.Reason
        };

        await _unitOfWork.Repository<Leave>().AddAsync(leave, cancellationToken);
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
