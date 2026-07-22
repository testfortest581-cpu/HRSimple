using FluentAssertions;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Tests.Domain;

public class BranchTests
{
    [Fact]
    public void CreateBranch_SetsActive_ByDefault()
    {
        var branch = new Branch
        {
            Name = "Tehran Branch",
            Code = "THR-01",
            Address = "Tehran, Iran",
            Phone = "02112345678"
        };

        branch.IsActive.Should().BeTrue();
        branch.Shifts.Should().BeEmpty();
        branch.Employees.Should().BeEmpty();
    }

    [Fact]
    public void Branch_HasCodeAndName()
    {
        var branch = new Branch { Name = "Main", Code = "BR-001" };
        branch.Code.Should().Be("BR-001");
        branch.Name.Should().Be("Main");
    }
}
