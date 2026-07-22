using MediatR;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Enums;

namespace RestaurantHR.Application.Features.Attendance.Commands.UpdateAttendance;

public record UpdateAttendanceCommand : IRequest<AttendanceDto>
{
    public Guid Id { get; init; }
    public Guid EmployeeId { get; init; }
    public DateTime Date { get; init; }
    public DateTime? CheckIn { get; init; }
    public DateTime? CheckOut { get; init; }
    public AttendanceStatus Status { get; init; }
}
