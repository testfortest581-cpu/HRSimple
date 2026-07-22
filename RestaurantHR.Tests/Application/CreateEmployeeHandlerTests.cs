using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantHR.Application;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.Features.Branches.Commands.CreateBranch;
using RestaurantHR.Application.Features.Employees.Commands.CreateEmployee;
using RestaurantHR.Application.Features.Employees.Queries.GetAllEmployees;
using RestaurantHR.Domain.Enums;
using RestaurantHR.Infrastructure.Data;
using RestaurantHR.Infrastructure.Repositories;

namespace RestaurantHR.Tests.Application;

public class CreateEmployeeHandlerTests
{
    private static ServiceProvider CreateServiceProvider()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddApplication();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        var context = new AppDbContext(options);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();

        services.AddSingleton(context);
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services.BuildServiceProvider();
    }

    [Fact]
    public async Task CreateEmployeeCommand_CreatesEmployeeSuccessfully()
    {
        using var scope = CreateServiceProvider().CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var branch = await mediator.Send(new CreateBranchCommand
        {
            Name = "Branch",
            Code = "BR-01"
        });

        var command = new CreateEmployeeCommand
        {
            FirstName = "Ali",
            LastName = "Mohammadi",
            NationalCode = "1234567890",
            Phone = "09121234567",
            Email = "ali@test.com",
            Role = EmployeeRole.Server,
            HireDate = new DateTime(2024, 1, 1),
            BaseSalary = 5000000,
            BranchId = branch.Id
        };

        var result = await mediator.Send(command);

        result.Should().NotBeNull();
        result.FirstName.Should().Be("Ali");
        result.Role.Should().Be(EmployeeRole.Server);
        result.BaseSalary.Should().Be(5000000);
    }

    [Fact]
    public async Task GetAllEmployeesQuery_ReturnsList()
    {
        using var scope = CreateServiceProvider().CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var branch = await mediator.Send(new CreateBranchCommand { Name = "B", Code = "B01" });

        await mediator.Send(new CreateEmployeeCommand
        {
            FirstName = "A", LastName = "B", NationalCode = "111",
            Role = EmployeeRole.Cook, BranchId = branch.Id
        });
        await mediator.Send(new CreateEmployeeCommand
        {
            FirstName = "C", LastName = "D", NationalCode = "222",
            Role = EmployeeRole.Cashier, BranchId = branch.Id
        });

        var employees = await mediator.Send(new GetAllEmployeesQuery());
        employees.Should().HaveCount(2);
    }
}
