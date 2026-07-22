using FluentAssertions;
using RestaurantHR.Domain.Entities;
using RestaurantHR.Domain.Enums;

namespace RestaurantHR.Tests.Domain;

public class LeaveTests
{
    [Fact]
    public void CreateLeave_SetsPendingStatus_ByDefault()
    {
        var leave = new Leave
        {
            EmployeeId = Guid.NewGuid(),
            StartDate = new DateTime(2025, 1, 1),
            EndDate = new DateTime(2025, 1, 5),
            LeaveType = LeaveType.Annual,
            Reason = "Personal"
        };

        leave.Status.Should().Be(LeaveStatus.Pending);
        leave.ApprovedById.Should().BeNull();
    }

    [Fact]
    public void SickLeave_HasCorrectType()
    {
        var leave = new Leave { LeaveType = LeaveType.Sick };
        leave.LeaveType.Should().Be(LeaveType.Sick);
    }

    [Fact]
    public void LeaveDates_StartBeforeEnd()
    {
        var leave = new Leave
        {
            StartDate = new DateTime(2025, 1, 1),
            EndDate = new DateTime(2025, 1, 10)
        };

        leave.EndDate.Should().BeAfter(leave.StartDate);
    }
}
