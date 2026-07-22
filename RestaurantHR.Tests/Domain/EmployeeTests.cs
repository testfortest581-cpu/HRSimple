using FluentAssertions;
using RestaurantHR.Domain.Entities;
using RestaurantHR.Domain.Enums;

namespace RestaurantHR.Tests.Domain;

public class EmployeeTests
{
    [Fact]
    public void CreateEmployee_SetsPropertiesCorrectly()
    {
        var employee = new Employee
        {
            FirstName = "Ali",
            LastName = "Mohammadi",
            NationalCode = "1234567890",
            Phone = "09121234567",
            Email = "ali@test.com",
            Role = EmployeeRole.Server,
            HireDate = new DateTime(2024, 1, 1),
            BaseSalary = 5000000,
            BranchId = Guid.NewGuid()
        };

        employee.FirstName.Should().Be("Ali");
        employee.LastName.Should().Be("Mohammadi");
        employee.NationalCode.Should().Be("1234567890");
        employee.Role.Should().Be(EmployeeRole.Server);
        employee.BaseSalary.Should().Be(5000000);
        employee.IsActive.Should().BeTrue();
        employee.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void Employee_HasEmptyCollections_ByDefault()
    {
        var employee = new Employee();
        employee.Assignments.Should().BeEmpty();
        employee.Leaves.Should().BeEmpty();
        employee.Attendances.Should().BeEmpty();
        employee.Salaries.Should().BeEmpty();
        employee.Payments.Should().BeEmpty();
    }
}
