using RestaurantHR.Domain.Common;

namespace RestaurantHR.Domain.Entities;

public class EmployeeAssignment : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public Guid ShiftId { get; set; }
    public Guid BranchId { get; set; }
    public string WorkDays { get; set; } = "[]";
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; } = true;

    public Employee Employee { get; set; } = null!;
    public Shift Shift { get; set; } = null!;
    public Branch Branch { get; set; } = null!;
}
