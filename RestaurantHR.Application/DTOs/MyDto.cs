namespace RestaurantHR.Application.DTOs;

public class MyLeaveDto
{
    public Guid Id { get; set; }
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string LeaveType { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}

public class MyPaymentDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string PaymentDate { get; set; } = string.Empty;
    public string PaymentType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class MyAssignmentDto
{
    public Guid Id { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public string ShiftName { get; set; } = string.Empty;
    public string[] WorkDays { get; set; } = [];
    public string WeekStartDate { get; set; } = string.Empty;
}