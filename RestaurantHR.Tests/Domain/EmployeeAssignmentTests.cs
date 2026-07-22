using FluentAssertions;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Tests.Domain;

public class EmployeeAssignmentTests
{
    [Fact]
    public void CreateAssignment_SetsActive_ByDefault()
    {
        var assignment = new EmployeeAssignment
        {
            EmployeeId = Guid.NewGuid(),
            ShiftId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            StartDate = new DateTime(2025, 1, 1)
        };

        assignment.IsActive.Should().BeTrue();
        assignment.EndDate.Should().BeNull();
    }

    [Fact]
    public void Assignment_WithEndDate_HasLimitedDuration()
    {
        var assignment = new EmployeeAssignment
        {
            StartDate = new DateTime(2025, 1, 1),
            EndDate = new DateTime(2025, 3, 31)
        };

        assignment.EndDate.Should().BeAfter(assignment.StartDate);
    }
}
