using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RestaurantHR.Domain.Entities;
using RestaurantHR.Infrastructure.Data;
using RestaurantHR.Infrastructure.Repositories;

namespace RestaurantHR.Tests.Infrastructure;

public class GenericRepositoryTests
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
    public async Task AddAsync_AddsEntity()
    {
        using var context = CreateDbContext();
        var repo = new GenericRepository<Branch>(context);

        var branch = new Branch { Name = "Test", Code = "T-01" };
        var result = await repo.AddAsync(branch);
        await context.SaveChangesAsync();

        result.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
    {
        using var context = CreateDbContext();
        var repo = new GenericRepository<Branch>(context);

        var result = await repo.GetByIdAsync(Guid.NewGuid());
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllEntities()
    {
        using var context = CreateDbContext();
        var repo = new GenericRepository<Branch>(context);

        await repo.AddAsync(new Branch { Name = "B1", Code = "C1" });
        await repo.AddAsync(new Branch { Name = "B2", Code = "C2" });
        await context.SaveChangesAsync();

        var result = await repo.GetAllAsync();
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task FindAsync_ReturnsFilteredEntities()
    {
        using var context = CreateDbContext();
        var repo = new GenericRepository<Branch>(context);

        await repo.AddAsync(new Branch { Name = "Active", Code = "A01", IsActive = true });
        await repo.AddAsync(new Branch { Name = "Inactive", Code = "I01", IsActive = false });
        await context.SaveChangesAsync();

        var result = await repo.FindAsync(b => b.IsActive);
        result.Should().HaveCount(1);
        result[0].Name.Should().Be("Active");
    }

    [Fact]
    public async Task Update_ModifiesEntity()
    {
        using var context = CreateDbContext();
        var repo = new GenericRepository<Branch>(context);

        var branch = new Branch { Name = "Old", Code = "U01" };
        await repo.AddAsync(branch);
        await context.SaveChangesAsync();

        branch.Name = "New";
        repo.Update(branch);
        await context.SaveChangesAsync();

        var retrieved = await repo.GetByIdAsync(branch.Id);
        retrieved!.Name.Should().Be("New");
    }

    [Fact]
    public async Task Delete_RemovesEntity()
    {
        using var context = CreateDbContext();
        var repo = new GenericRepository<Branch>(context);

        var branch = new Branch { Name = "Delete Me", Code = "D01" };
        await repo.AddAsync(branch);
        await context.SaveChangesAsync();

        repo.Delete(branch);
        await context.SaveChangesAsync();

        var result = await repo.GetByIdAsync(branch.Id);
        result.Should().BeNull();
    }

    [Fact]
    public async Task ExistsAsync_ReturnsCorrectBoolean()
    {
        using var context = CreateDbContext();
        var repo = new GenericRepository<Branch>(context);

        var branch = new Branch { Name = "Exists", Code = "E01" };
        await repo.AddAsync(branch);
        await context.SaveChangesAsync();

        var exists = await repo.ExistsAsync(b => b.Code == "E01");
        exists.Should().BeTrue();

        var notExists = await repo.ExistsAsync(b => b.Code == "NONEXIST");
        notExists.Should().BeFalse();
    }
}
