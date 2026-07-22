using MediatR;

namespace RestaurantHR.Application.Features.Attendance.Commands.DeleteAttendance;

public record DeleteAttendanceCommand(Guid Id) : IRequest;
