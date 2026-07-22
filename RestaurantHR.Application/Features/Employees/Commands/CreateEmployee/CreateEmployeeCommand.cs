using MediatR;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Enums;

namespace RestaurantHR.Application.Features.Employees.Commands.CreateEmployee;

public record CreateEmployeeCommand : IRequest<EmployeeDto>
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string NationalCode { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public EmployeeRole Role { get; init; }
    public DateTime HireDate { get; init; }
    public decimal BaseSalary { get; init; }
    public Guid? BranchId { get; init; }
}
