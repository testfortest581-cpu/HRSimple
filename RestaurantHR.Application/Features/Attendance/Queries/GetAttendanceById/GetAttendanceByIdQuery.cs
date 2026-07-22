using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Attendance.Queries.GetAttendanceById;

public record GetAttendanceByIdQuery(Guid Id) : IRequest<AttendanceDto?>;
