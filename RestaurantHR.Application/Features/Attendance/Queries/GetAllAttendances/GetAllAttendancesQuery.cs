using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Attendance.Queries.GetAllAttendances;

public record GetAllAttendancesQuery : IRequest<List<AttendanceDto>>;
