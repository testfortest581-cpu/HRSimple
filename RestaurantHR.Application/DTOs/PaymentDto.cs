using RestaurantHR.Domain.Enums;

namespace RestaurantHR.Application.DTOs;

public class PaymentDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentType PaymentType { get; set; }
    public string Description { get; set; } = string.Empty;
    public Guid? SalaryId { get; set; }
}
