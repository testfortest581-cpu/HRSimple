using FluentAssertions;
using Moq;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.Features.Branches.Commands.CreateBranch;
using RestaurantHR.Application.Features.Branches.Queries.GetAllBranches;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Tests.Application;

public class BranchHandlerMoqTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IGenericRepository<Branch>> _repositoryMock;

    public BranchHandlerMoqTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _repositoryMock = new Mock<IGenericRepository<Branch>>();

        _unitOfWorkMock.Setup(u => u.Repository<Branch>()).Returns(_repositoryMock.Object);
    }

    [Fact]
    public async Task CreateBranchHandler_ShouldAddBranchAndReturnDto()
    {
        var handler = new CreateBranchHandler(_unitOfWorkMock.Object);
        var command = new CreateBranchCommand
        {
            Name = "Test Branch",
            Code = "TST-01",
            Address = "Test Address"
        };

        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Branch>(), default))
            .ReturnsAsync((Branch b, CancellationToken _) => b);

        var result = await handler.Handle(command, default);

        result.Should().NotBeNull();
        result.Name.Should().Be("Test Branch");
        result.Code.Should().Be("TST-01");
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Branch>(), default), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task GetAllBranchesHandler_ShouldReturnList()
    {
        var handler = new GetAllBranchesHandler(_unitOfWorkMock.Object);
        var branches = new List<Branch> { new Branch { Name = "Branch 1", Code = "B1", IsActive = true }, new Branch { Name = "Branch 2", Code = "B2", IsActive = true }, new Branch { Name = "Branch 3", Code = "B3", IsActive = false } };

        _repositoryMock.Setup(r => r.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Branch, bool>>>(), default))
            .ReturnsAsync(branches);

        var result = await handler.Handle(new GetAllBranchesQuery(), default);

        result.Should().HaveCount(3);
        result[0].Name.Should().Be("Branch 1");
        result[1].IsActive.Should().BeTrue();
        result[2].IsActive.Should().BeFalse();
    }

    [Fact]
    public async Task CreateBranchHandler_ShouldIgnoreId_FromCommand()
    {
        var handler = new CreateBranchHandler(_unitOfWorkMock.Object);
        var command = new CreateBranchCommand
        {
            Name = "Auto Id",
            Code = "AUTO-01"
        };

        Branch? capturedBranch = null;
        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Branch>(), default))
            .Callback((Branch b, CancellationToken _) => capturedBranch = b)
            .ReturnsAsync((Branch b, CancellationToken _) => b);

        var result = await handler.Handle(command, default);

        result.Id.Should().NotBeEmpty();
        capturedBranch.Should().NotBeNull();
        capturedBranch!.Id.Should().NotBeEmpty();
        capturedBranch.Name.Should().Be("Auto Id");
    }
}
