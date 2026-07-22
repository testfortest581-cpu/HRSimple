using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Salary.Commands.UpdateSalary;

public record UpdateSalaryCommand : IRequest<SalaryDto>
{
    public Guid Id { get; init; }
    public Guid EmployeeId { get; init; }
    public int Month { get; init; }
    public int Year { get; init; }
    public decimal BaseSalary { get; init; }
    public decimal Overtime { get; init; }
    public decimal Bonus { get; init; }
    public decimal Deductions { get; init; }
    public bool IsPaid { get; init; }
    public DateTime? PaidAt { get; init; }
}
