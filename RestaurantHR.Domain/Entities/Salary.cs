using RestaurantHR.Domain.Common;

namespace RestaurantHR.Domain.Entities;

public class Salary : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal BaseSalary { get; set; }
    public decimal Overtime { get; set; }
    public decimal Bonus { get; set; }
    public decimal Deductions { get; set; }
    public decimal NetSalary => BaseSalary + Overtime + Bonus - Deductions;
    public bool IsPaid { get; set; }
    public DateTime? PaidAt { get; set; }
    public string Notes { get; set; } = string.Empty;

    public Employee Employee { get; set; } = null!;
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
