using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Application.Features.Branches.Commands.CreateBranch;
using RestaurantHR.Infrastructure.Data;

namespace RestaurantHR.Tests.Api;

public class BranchesControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public BranchesControllerTests(WebApplicationFactory<Program> factory)
    {
        var dbPath = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.db");
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.UseSetting("ConnectionStrings:DefaultConnection", $"DataSource={dbPath}");

            builder.ConfigureServices(services =>
            {
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.EnsureCreated();
            });
        });
    }

    [Fact]
    public async Task GetAll_ReturnsEmptyList_Initially()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/branches");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var branches = await response.Content.ReadFromJsonAsync<List<BranchDto>>();
        branches.Should().BeEmpty();
    }

    [Fact]
    public async Task Create_ReturnsCreatedBranch()
    {
        var client = _factory.CreateClient();

        var command = new CreateBranchCommand
        {
            Name = "Test Branch",
            Code = "TST-01",
            Address = "Test Address"
        };

        var response = await client.PostAsJsonAsync("/api/branches", command);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var branch = await response.Content.ReadFromJsonAsync<BranchDto>();
        branch.Should().NotBeNull();
        branch!.Name.Should().Be("Test Branch");
        branch.Code.Should().Be("TST-01");
    }

    [Fact]
    public async Task GetById_ReturnsBranch()
    {
        var client = _factory.CreateClient();

        var createResponse = await client.PostAsJsonAsync("/api/branches",
            new CreateBranchCommand { Name = "Branch", Code = "BR-01" });
        var created = await createResponse.Content.ReadFromJsonAsync<BranchDto>();

        var response = await client.GetAsync($"/api/branches/{created!.Id}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var branch = await response.Content.ReadFromJsonAsync<BranchDto>();
        branch!.Name.Should().Be("Branch");
    }

    [Fact]
    public async Task GetById_Returns404_ForNonExistent()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/api/branches/{Guid.NewGuid()}");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Create_WithInvalidData_Returns400()
    {
        var client = _factory.CreateClient();

        var command = new CreateBranchCommand { Name = "", Code = "" };
        var response = await client.PostAsJsonAsync("/api/branches", command);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Delete_RemovesBranch()
    {
        var client = _factory.CreateClient();

        var createResponse = await client.PostAsJsonAsync("/api/branches",
            new CreateBranchCommand { Name = "Delete Me", Code = "DEL-01" });
        var created = await createResponse.Content.ReadFromJsonAsync<BranchDto>();

        var deleteResponse = await client.DeleteAsync($"/api/branches/{created!.Id}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var getResponse = await client.GetAsync($"/api/branches/{created.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var deletedBranch = await getResponse.Content.ReadFromJsonAsync<BranchDto>();
        deletedBranch!.IsActive.Should().BeFalse();
    }
}
