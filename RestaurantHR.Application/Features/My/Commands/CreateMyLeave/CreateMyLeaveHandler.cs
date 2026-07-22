using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;
using RestaurantHR.Domain.Enums;

namespace RestaurantHR.Application.Features.My.Commands.CreateMyLeave;

public class CreateMyLeaveHandler : IRequestHandler<CreateMyLeaveCommand, MyLeaveDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMyLeaveHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<MyLeaveDto> Handle(CreateMyLeaveCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<LeaveType>(request.LeaveType, out var leaveType))
            leaveType = LeaveType.Annual;

        var leave = new Leave
        {
            EmployeeId = request.EmployeeId,
            StartDate = DateTime.Parse(request.StartDate),
            EndDate = DateTime.Parse(request.EndDate),
            LeaveType = leaveType,
            Reason = request.Reason,
            Status = LeaveStatus.Pending,
        };

        await _unitOfWork.Repository<Leave>().AddAsync(leave, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new MyLeaveDto
        {
            Id = leave.Id,
            StartDate = leave.StartDate.ToString("yyyy-MM-dd"),
            EndDate = leave.EndDate.ToString("yyyy-MM-dd"),
            LeaveType = leave.LeaveType.ToString(),
            Reason = leave.Reason,
            Status = leave.Status.ToString(),
        };
    }
}