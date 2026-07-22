using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.WeeklyAssignments.Queries.GetWeeklyAssignmentById;

public record GetWeeklyAssignmentByIdQuery(Guid Id) : IRequest<WeeklyAssignmentDto?>;
