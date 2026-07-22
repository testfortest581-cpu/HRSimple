namespace RestaurantHR.Application.DTOs;

public class WeeklyAssignmentDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public Guid ShiftId { get; set; }
    public string ShiftName { get; set; } = string.Empty;
    public string WorkDays { get; set; } = "[]";
    public DateTime WeekStartDate { get; set; }
    public bool IsActive { get; set; }
}
