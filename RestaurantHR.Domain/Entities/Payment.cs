using RestaurantHR.Domain.Common;
using RestaurantHR.Domain.Enums;

namespace RestaurantHR.Domain.Entities;

public class Payment : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentType PaymentType { get; set; }
    public string Description { get; set; } = string.Empty;
    public Guid? SalaryId { get; set; }

    public Employee Employee { get; set; } = null!;
    public Salary? Salary { get; set; }
}
