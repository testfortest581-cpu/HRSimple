using FluentAssertions;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Tests.Domain;

public class ShiftTests
{
    [Fact]
    public void CreateShift_SetsTimesCorrectly()
    {
        var shift = new Shift
        {
            BranchId = Guid.NewGuid(),
            Name = "Morning",
            StartTime = new TimeSpan(8, 0, 0),
            EndTime = new TimeSpan(14, 0, 0)
        };

        shift.Name.Should().Be("Morning");
        shift.StartTime.Should().Be(new TimeSpan(8, 0, 0));
        shift.EndTime.Should().Be(new TimeSpan(14, 0, 0));
        shift.IsActive.Should().BeTrue();
    }
}
