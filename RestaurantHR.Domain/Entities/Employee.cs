using RestaurantHR.Domain.Common;
using RestaurantHR.Domain.Enums;

namespace RestaurantHR.Domain.Entities;

public class Employee : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string NationalCode { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public EmployeeRole Role { get; set; }
    public DateTime HireDate { get; set; }
    public decimal BaseSalary { get; set; }
    public bool IsActive { get; set; } = true;
    public Guid? BranchId { get; set; }

    public Branch? Branch { get; set; }
    public ICollection<EmployeeAssignment> Assignments { get; set; } = new List<EmployeeAssignment>();
    public ICollection<Leave> Leaves { get; set; } = new List<Leave>();
    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    public ICollection<Salary> Salaries { get; set; } = new List<Salary>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
