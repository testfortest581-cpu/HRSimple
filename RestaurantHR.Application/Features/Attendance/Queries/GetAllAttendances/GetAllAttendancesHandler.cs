using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;
using AttendanceEntity = RestaurantHR.Domain.Entities.Attendance;

namespace RestaurantHR.Application.Features.Attendance.Queries.GetAllAttendances;

public class GetAllAttendancesHandler : IRequestHandler<GetAllAttendancesQuery, List<AttendanceDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllAttendancesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<AttendanceDto>> Handle(GetAllAttendancesQuery request, CancellationToken cancellationToken)
    {
        var employees = await _unitOfWork.Repository<Employee>().GetAllAsync(cancellationToken);
        var employeeMap = employees.ToDictionary(e => e.Id, e => $"{e.FirstName} {e.LastName}");

        var attendances = await _unitOfWork.Repository<AttendanceEntity>().GetAllAsync(cancellationToken);
        return attendances.Select(a => new AttendanceDto
        {
            Id = a.Id,
            EmployeeId = a.EmployeeId,
            EmployeeName = employeeMap.GetValueOrDefault(a.EmployeeId, string.Empty),
            Date = a.Date,
            CheckIn = a.CheckIn,
            CheckOut = a.CheckOut,
            Status = a.Status
        }).ToList();
    }
}
