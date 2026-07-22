using FluentAssertions;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Tests.Domain;

public class SalaryTests
{
    [Fact]
    public void NetSalary_CalculatesCorrectly()
    {
        var salary = new Salary
        {
            BaseSalary = 5000000,
            Overtime = 500000,
            Bonus = 1000000,
            Deductions = 300000
        };

        salary.NetSalary.Should().Be(6200000);
    }

    [Fact]
    public void NetSalary_WithZeroBonus_ReturnsBasePlusOvertime()
    {
        var salary = new Salary
        {
            BaseSalary = 5000000,
            Overtime = 500000,
            Bonus = 0,
            Deductions = 0
        };

        salary.NetSalary.Should().Be(5500000);
    }

    [Fact]
    public void NetSalary_WhenDeductionsExceed_ReturnsNegative()
    {
        var salary = new Salary
        {
            BaseSalary = 1000000,
            Overtime = 0,
            Bonus = 0,
            Deductions = 2000000
        };

        salary.NetSalary.Should().Be(-1000000);
    }

    [Fact]
    public void Salary_IsNotPaid_ByDefault()
    {
        var salary = new Salary();
        salary.IsPaid.Should().BeFalse();
        salary.PaidAt.Should().BeNull();
    }
}
