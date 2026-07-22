using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Salary.Queries.GetAllSalaries;

public record GetAllSalariesQuery : IRequest<List<SalaryDto>>;
