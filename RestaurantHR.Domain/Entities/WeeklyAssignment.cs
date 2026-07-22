using RestaurantHR.Domain.Common;

namespace RestaurantHR.Domain.Entities;

public class WeeklyAssignment : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public Guid BranchId { get; set; }
    public Guid ShiftId { get; set; }
    public string WorkDays { get; set; } = "[]";
    public DateTime WeekStartDate { get; set; }
    public bool IsActive { get; set; } = true;

    public Employee Employee { get; set; } = null!;
    public Branch Branch { get; set; } = null!;
    public Shift Shift { get; set; } = null!;
}
