using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.WeeklyAssignments.Queries.GetAllWeeklyAssignments;

public record GetAllWeeklyAssignmentsQuery : IRequest<List<WeeklyAssignmentDto>>;
