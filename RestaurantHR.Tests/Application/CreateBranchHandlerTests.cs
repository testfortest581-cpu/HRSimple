using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantHR.Application;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.Features.Branches.Commands.CreateBranch;
using RestaurantHR.Application.Features.Branches.Queries.GetAllBranches;
using RestaurantHR.Domain.Entities;
using RestaurantHR.Infrastructure.Data;
using RestaurantHR.Infrastructure.Repositories;

namespace RestaurantHR.Tests.Application;

public class CreateBranchHandlerTests
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
    public async Task CreateBranchCommand_CreatesBranchSuccessfully()
    {
        using var scope = CreateServiceProvider().CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var command = new CreateBranchCommand
        {
            Name = "Tehran Branch",
            Code = "THR-01",
            Address = "Tehran, Iran",
            Phone = "02112345678"
        };

        var result = await mediator.Send(command);

        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        result.Name.Should().Be("Tehran Branch");
        result.Code.Should().Be("THR-01");
        result.IsActive.Should().BeTrue();
    }

    [Fact]
    public async Task GetAllBranchesQuery_ReturnsList()
    {
        using var scope = CreateServiceProvider().CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        await mediator.Send(new CreateBranchCommand { Name = "B1", Code = "C1" });
        await mediator.Send(new CreateBranchCommand { Name = "B2", Code = "C2" });

        var branches = await mediator.Send(new GetAllBranchesQuery());

        branches.Should().HaveCount(2);
    }

    [Fact]
    public async Task CreateBranchCommand_WithEmptyName_ThrowsValidationError()
    {
        using var scope = CreateServiceProvider().CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var command = new CreateBranchCommand
        {
            Name = "",
            Code = "C1",
            Address = "Test"
        };

        Func<Task> act = () => mediator.Send(command);
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }
}
