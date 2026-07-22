using RestaurantHR.Domain.Enums;

namespace RestaurantHR.Application.DTOs;

public class EmployeeDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string NationalCode { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public EmployeeRole Role { get; set; }
    public DateTime HireDate { get; set; }
    public decimal BaseSalary { get; set; }
    public bool IsActive { get; set; }
    public Guid? BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
}
