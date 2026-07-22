using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.EmployeeAssignments.Queries.GetEmployeeAssignmentById;

public record GetEmployeeAssignmentByIdQuery(Guid Id) : IRequest<EmployeeAssignmentDto?>;
