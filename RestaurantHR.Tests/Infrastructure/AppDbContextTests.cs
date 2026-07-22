using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RestaurantHR.Domain.Entities;
using RestaurantHR.Domain.Enums;
using RestaurantHR.Infrastructure.Data;

namespace RestaurantHR.Tests.Infrastructure;

public class AppDbContextTests
{
    private static AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        var context = new AppDbContext(options);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();
        return context;
    }

    [Fact]
    public async Task CanAddAndRetrieveBranch()
    {
        using var context = CreateDbContext();

        var branch = new Branch
        {
            Name = "Test Branch",
            Code = "TST-01",
            Address = "Test Address"
        };

        context.Branches.Add(branch);
        await context.SaveChangesAsync();

        var retrieved = await context.Branches.FindAsync(branch.Id);
        retrieved.Should().NotBeNull();
        retrieved!.Name.Should().Be("Test Branch");
        retrieved.Code.Should().Be("TST-01");
    }

    [Fact]
    public async Task CanAddAndRetrieveEmployee()
    {
        using var context = CreateDbContext();

        var branch = new Branch { Name = "Branch", Code = "BR-01" };
        context.Branches.Add(branch);
        await context.SaveChangesAsync();

        var employee = new Employee
        {
            FirstName = "John",
            LastName = "Doe",
            NationalCode = "1234567890",
            BranchId = branch.Id,
            BaseSalary = 5000000
        };

        context.Employees.Add(employee);
        await context.SaveChangesAsync();

        var retrieved = await context.Employees.FindAsync(employee.Id);
        retrieved.Should().NotBeNull();
        retrieved!.FirstName.Should().Be("John");
        retrieved.BranchId.Should().Be(branch.Id);
    }

    [Fact]
    public async Task CanAddShiftWithBranch()
    {
        using var context = CreateDbContext();

        var branch = new Branch { Name = "Branch", Code = "BR-02" };
        context.Branches.Add(branch);
        await context.SaveChangesAsync();

        var shift = new Shift
        {
            Name = "Morning",
            BranchId = branch.Id,
            StartTime = new TimeSpan(8, 0, 0),
            EndTime = new TimeSpan(14, 0, 0)
        };

        context.Shifts.Add(shift);
        await context.SaveChangesAsync();

        var retrieved = await context.Shifts.FindAsync(shift.Id);
        retrieved.Should().NotBeNull();
        retrieved!.Name.Should().Be("Morning");
    }

    [Fact]
    public async Task CanAddLeaveWithEmployee()
    {
        using var context = CreateDbContext();

        var branch = new Branch { Name = "Branch", Code = "BR-03" };
        context.Branches.Add(branch);
        await context.SaveChangesAsync();

        var employee = new Employee
        {
            FirstName = "Jane",
            LastName = "Doe",
            NationalCode = "0987654321",
            BranchId = branch.Id
        };
        context.Employees.Add(employee);
        await context.SaveChangesAsync();

        var leave = new Leave
        {
            EmployeeId = employee.Id,
            StartDate = new DateTime(2025, 1, 1),
            EndDate = new DateTime(2025, 1, 5),
            LeaveType = LeaveType.Annual,
            Reason = "Vacation"
        };

        context.Leaves.Add(leave);
        await context.SaveChangesAsync();

        var retrieved = await context.Leaves.FindAsync(leave.Id);
        retrieved.Should().NotBeNull();
        retrieved!.Reason.Should().Be("Vacation");
        retrieved.Status.Should().Be(LeaveStatus.Pending);
    }

    [Fact]
    public async Task CanCreateFullEmployeeAssignment()
    {
        using var context = CreateDbContext();

        var branch = new Branch { Name = "Branch", Code = "BR-04" };
        context.Branches.Add(branch);
        await context.SaveChangesAsync();

        var employee = new Employee
        {
            FirstName = "Ali",
            LastName = "Rezaei",
            NationalCode = "1122334455",
            BranchId = branch.Id
        };
        context.Employees.Add(employee);

        var shift = new Shift
        {
            Name = "Evening",
            BranchId = branch.Id,
            StartTime = new TimeSpan(14, 0, 0),
            EndTime = new TimeSpan(22, 0, 0)
        };
        context.Shifts.Add(shift);
        await context.SaveChangesAsync();

        var assignment = new EmployeeAssignment
        {
            EmployeeId = employee.Id,
            ShiftId = shift.Id,
            BranchId = branch.Id,
            StartDate = new DateTime(2025, 1, 1)
        };
        context.EmployeeAssignments.Add(assignment);
        await context.SaveChangesAsync();

        var retrieved = await context.EmployeeAssignments.FindAsync(assignment.Id);
        retrieved.Should().NotBeNull();
        retrieved!.IsActive.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateTimestamp_OnEntityModification()
    {
        using var context = CreateDbContext();

        var branch = new Branch { Name = "Original", Code = "BR-05" };
        context.Branches.Add(branch);
        await context.SaveChangesAsync();

        var created = branch.CreatedAt;
        branch.Name = "Updated";
        await context.SaveChangesAsync();

        branch.UpdatedAt.Should().NotBeNull();
        branch.UpdatedAt!.Value.Should().BeAfter(created);
    }

    [Fact]
    public async Task Salary_CalculatesNetCorrectly_InDatabase()
    {
        using var context = CreateDbContext();

        var branch = new Branch { Name = "Branch", Code = "BR-06" };
        context.Branches.Add(branch);
        await context.SaveChangesAsync();

        var employee = new Employee
        {
            FirstName = "Test",
            LastName = "User",
            NationalCode = "5566778899",
            BranchId = branch.Id
        };
        context.Employees.Add(employee);
        await context.SaveChangesAsync();

        var salary = new Salary
        {
            EmployeeId = employee.Id,
            Month = 1,
            Year = 2025,
            BaseSalary = 5000000,
            Overtime = 500000,
            Bonus = 1000000,
            Deductions = 200000
        };
        context.Salaries.Add(salary);
        await context.SaveChangesAsync();

        salary.NetSalary.Should().Be(6300000);
    }

    [Fact]
    public async Task CanCreatePayment()
    {
        using var context = CreateDbContext();

        var branch = new Branch { Name = "Branch", Code = "BR-07" };
        context.Branches.Add(branch);
        await context.SaveChangesAsync();

        var employee = new Employee
        {
            FirstName = "Test",
            LastName = "User",
            NationalCode = "9988776655",
            BranchId = branch.Id
        };
        context.Employees.Add(employee);
        await context.SaveChangesAsync();

        var payment = new Payment
        {
            EmployeeId = employee.Id,
            Amount = 5000000,
            PaymentDate = DateTime.UtcNow,
            PaymentType = PaymentType.BankTransfer,
            Description = "January salary"
        };
        context.Payments.Add(payment);
        await context.SaveChangesAsync();

        var retrieved = await context.Payments.FindAsync(payment.Id);
        retrieved.Should().NotBeNull();
        retrieved!.Amount.Should().Be(5000000);
    }

    [Fact]
    public async Task UniqueCodeConstraint_OnBranch_Throws()
    {
        using var context = CreateDbContext();

        context.Branches.Add(new Branch { Name = "B1", Code = "SAME" });

        await context.SaveChangesAsync();

        context.Branches.Add(new Branch { Name = "B2", Code = "SAME" });
        Func<Task> act = () => context.SaveChangesAsync();

        await act.Should().ThrowAsync<DbUpdateException>();
    }
}
