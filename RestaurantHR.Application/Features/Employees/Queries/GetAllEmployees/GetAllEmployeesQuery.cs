using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Employees.Queries.GetAllEmployees;

public record GetAllEmployeesQuery : IRequest<List<EmployeeDto>>;
