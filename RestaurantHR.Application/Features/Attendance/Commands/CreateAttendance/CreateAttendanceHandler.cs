using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;
using AttendanceEntity = RestaurantHR.Domain.Entities.Attendance;

namespace RestaurantHR.Application.Features.Attendance.Commands.CreateAttendance;

public class CreateAttendanceHandler : IRequestHandler<CreateAttendanceCommand, AttendanceDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateAttendanceHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AttendanceDto> Handle(CreateAttendanceCommand request, CancellationToken cancellationToken)
    {
        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.EmployeeId, cancellationToken);
        if (employee is null)
            throw new KeyNotFoundException($"Employee with id {request.EmployeeId} not found");

        var attendance = new AttendanceEntity
        {
            EmployeeId = request.EmployeeId,
            Date = request.Date,
            CheckIn = request.CheckIn,
            CheckOut = request.CheckOut,
            Status = request.Status
        };

        await _unitOfWork.Repository<AttendanceEntity>().AddAsync(attendance, cancellationToken);
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
