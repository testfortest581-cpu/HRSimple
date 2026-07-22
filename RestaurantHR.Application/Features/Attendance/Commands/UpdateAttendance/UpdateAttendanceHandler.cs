using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;
using AttendanceEntity = RestaurantHR.Domain.Entities.Attendance;

namespace RestaurantHR.Application.Features.Attendance.Commands.UpdateAttendance;

public class UpdateAttendanceHandler : IRequestHandler<UpdateAttendanceCommand, AttendanceDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAttendanceHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AttendanceDto> Handle(UpdateAttendanceCommand request, CancellationToken cancellationToken)
    {
        var attendance = await _unitOfWork.Repository<AttendanceEntity>().GetByIdAsync(request.Id, cancellationToken);
        if (attendance is null)
            throw new KeyNotFoundException($"Attendance with id {request.Id} not found");

        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.EmployeeId, cancellationToken);
        if (employee is null)
            throw new KeyNotFoundException($"Employee with id {request.EmployeeId} not found");

        attendance.EmployeeId = request.EmployeeId;
        attendance.Date = request.Date;
        attendance.CheckIn = request.CheckIn;
        attendance.CheckOut = request.CheckOut;
        attendance.Status = request.Status;

        _unitOfWork.Repository<AttendanceEntity>().Update(attendance);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AttendanceDto
        {
            Id = attendance.Id,
            EmployeeId = attendance.EmployeeId,
            EmployeeName = $"{employee.FirstName} {employee.LastName}",
            Date = attendance.Date,
            CheckIn = attendance.CheckIn,
            CheckOut = attendance.CheckOut,
            Status = attendance.Status
        };
    }
}
