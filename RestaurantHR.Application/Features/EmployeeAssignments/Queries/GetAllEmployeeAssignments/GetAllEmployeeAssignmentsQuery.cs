using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.EmployeeAssignments.Queries.GetAllEmployeeAssignments;

public record GetAllEmployeeAssignmentsQuery : IRequest<List<EmployeeAssignmentDto>>;
