using RestaurantHR.Domain.Common;
using RestaurantHR.Domain.Enums;

namespace RestaurantHR.Domain.Entities;

public class Attendance : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public DateTime? CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
    public AttendanceStatus Status { get; set; }

    public Employee Employee { get; set; } = null!;
}
