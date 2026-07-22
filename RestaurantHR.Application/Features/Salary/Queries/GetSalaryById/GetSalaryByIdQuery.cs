using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Salary.Queries.GetSalaryById;

public record GetSalaryByIdQuery(Guid Id) : IRequest<SalaryDto?>;
