using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Employees.Queries.GetEmployeeById;

public record GetEmployeeByIdQuery(Guid Id) : IRequest<EmployeeDto?>;
