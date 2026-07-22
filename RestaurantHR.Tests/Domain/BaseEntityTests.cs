using FluentAssertions;
using RestaurantHR.Domain.Common;

namespace RestaurantHR.Tests.Domain;

public class BaseEntityTests
{
    private class TestEntity : BaseEntity { }

    [Fact]
    public void Constructor_SetsId_ToNonEmptyGuid()
    {
        var entity = new TestEntity();
        entity.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void Constructor_SetsCreatedAt_ToUtcNow()
    {
        var before = DateTime.UtcNow.AddSeconds(-1);
        var entity = new TestEntity();
        var after = DateTime.UtcNow.AddSeconds(1);

        entity.CreatedAt.Should().BeAfter(before).And.BeBefore(after);
        entity.CreatedAt.Kind.Should().Be(DateTimeKind.Utc);
    }

    [Fact]
    public void Constructor_SetsUpdatedAt_ToNull()
    {
        var entity = new TestEntity();
        entity.UpdatedAt.Should().BeNull();
    }

    [Fact]
    public void TwoEntities_HaveDifferentIds()
    {
        var entity1 = new TestEntity();
        var entity2 = new TestEntity();
        entity1.Id.Should().NotBe(entity2.Id);
    }
}
