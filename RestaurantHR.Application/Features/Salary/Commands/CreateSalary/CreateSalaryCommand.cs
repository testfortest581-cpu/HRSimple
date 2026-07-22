using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Salary.Commands.CreateSalary;

public record CreateSalaryCommand : IRequest<SalaryDto>
{
    public Guid EmployeeId { get; init; }
    public int Month { get; init; }
    public int Year { get; init; }
    public decimal BaseSalary { get; init; }
    public decimal Overtime { get; init; }
    public decimal Bonus { get; init; }
    public decimal Deductions { get; init; }
    public bool IsPaid { get; init; }
}
