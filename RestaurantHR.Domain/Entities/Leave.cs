using RestaurantHR.Domain.Common;
using RestaurantHR.Domain.Enums;

namespace RestaurantHR.Domain.Entities;

public class Leave : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public LeaveType LeaveType { get; set; }
    public string Reason { get; set; } = string.Empty;
    public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
    public Guid? ApprovedById { get; set; }

    public Employee Employee { get; set; } = null!;
    public Employee? ApprovedBy { get; set; }
}
