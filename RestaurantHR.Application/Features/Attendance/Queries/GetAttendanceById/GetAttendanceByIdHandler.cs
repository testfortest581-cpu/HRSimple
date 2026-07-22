using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;
using AttendanceEntity = RestaurantHR.Domain.Entities.Attendance;

namespace RestaurantHR.Application.Features.Attendance.Queries.GetAttendanceById;

public class GetAttendanceByIdHandler : IRequestHandler<GetAttendanceByIdQuery, AttendanceDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAttendanceByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AttendanceDto?> Handle(GetAttendanceByIdQuery request, CancellationToken cancellationToken)
    {
        var attendance = await _unitOfWork.Repository<AttendanceEntity>().GetByIdAsync(request.Id, cancellationToken);
        if (attendance is null) return null;

        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(attendance.EmployeeId, cancellationToken);

        return new AttendanceDto
        {
            Id = attendance.Id,
            EmployeeId = attendance.EmployeeId,
            EmployeeName = employee is not null ? $"{employee.FirstName} {employee.LastName}" : string.Empty,
            Date = attendance.Date,
            CheckIn = attendance.CheckIn,
            CheckOut = attendance.CheckOut,
            Status = attendance.Status
        };
    }
}
