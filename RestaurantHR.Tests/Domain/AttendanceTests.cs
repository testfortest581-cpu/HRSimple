using FluentAssertions;
using RestaurantHR.Domain.Entities;
using RestaurantHR.Domain.Enums;

namespace RestaurantHR.Tests.Domain;

public class AttendanceTests
{
    [Fact]
    public void CreateAttendance_WithPresentStatus()
    {
        var attendance = new Attendance
        {
            EmployeeId = Guid.NewGuid(),
            Date = new DateTime(2025, 1, 15),
            CheckIn = new DateTime(2025, 1, 15, 8, 0, 0),
            CheckOut = new DateTime(2025, 1, 15, 16, 0, 0),
            Status = AttendanceStatus.Present
        };

        attendance.Status.Should().Be(AttendanceStatus.Present);
        attendance.CheckIn.Should().NotBeNull();
        attendance.CheckOut.Should().NotBeNull();
    }

    [Fact]
    public void AbsentAttendance_HasNoCheckTimes()
    {
        var attendance = new Attendance
        {
            EmployeeId = Guid.NewGuid(),
            Date = new DateTime(2025, 1, 15),
            Status = AttendanceStatus.Absent
        };

        attendance.CheckIn.Should().BeNull();
        attendance.CheckOut.Should().BeNull();
    }
}
