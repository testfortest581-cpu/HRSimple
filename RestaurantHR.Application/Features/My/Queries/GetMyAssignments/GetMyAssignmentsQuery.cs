using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.My.Queries.GetMyAssignments;

public record GetMyAssignmentsQuery(Guid EmployeeId) : IRequest<List<MyAssignmentDto>>;