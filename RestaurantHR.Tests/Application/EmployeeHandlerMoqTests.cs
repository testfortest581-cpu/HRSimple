using FluentAssertions;
using Moq;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.Features.Employees.Commands.CreateEmployee;
using RestaurantHR.Application.Features.Employees.Queries.GetAllEmployees;
using RestaurantHR.Domain.Entities;
using RestaurantHR.Domain.Enums;

namespace RestaurantHR.Tests.Application;

public class EmployeeHandlerMoqTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IGenericRepository<Employee>> _repositoryMock;

    public EmployeeHandlerMoqTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _repositoryMock = new Mock<IGenericRepository<Employee>>();
        _unitOfWorkMock.Setup(u => u.Repository<Employee>()).Returns(_repositoryMock.Object);
    }

    [Fact]
    public async Task CreateEmployeeHandler_ShouldCreateEmployee()
    {
        var handler = new CreateEmployeeHandler(_unitOfWorkMock.Object);
        var command = new CreateEmployeeCommand
        {
            FirstName = "Ali",
            LastName = "Mohammadi",
            NationalCode = "1234567890",
            Role = EmployeeRole.Server,
            BaseSalary = 5000000,
            BranchId = Guid.NewGuid()
        };

        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Employee>(), default))
            .ReturnsAsync((Employee e, CancellationToken _) => e);

        var result = await handler.Handle(command, default);

        result.FirstName.Should().Be("Ali");
        result.Role.Should().Be(EmployeeRole.Server);
        result.BaseSalary.Should().Be(5000000);
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Employee>(), default), Times.Once);
    }

    [Fact]
    public async Task GetAllEmployeesHandler_ShouldReturnFilteredEmployees()
    {
        var handler = new GetAllEmployeesHandler(_unitOfWorkMock.Object);
        var employees = new List<Employee> { new Employee { FirstName = "Ali", LastName = "Mohammadi", NationalCode = "001", Role = EmployeeRole.Manager, IsActive = true, BranchId = Guid.NewGuid() }, new Employee { FirstName = "Sara", LastName = "Ahmadi", NationalCode = "002", Role = EmployeeRole.Server, IsActive = true, BranchId = Guid.NewGuid() }, new Employee { FirstName = "Reza", LastName = "Hosseini", NationalCode = "003", Role = EmployeeRole.Chef, IsActive = false, BranchId = Guid.NewGuid() } };

        _repositoryMock.Setup(r => r.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Employee, bool>>>(), default))
            .ReturnsAsync(employees);

        var result = await handler.Handle(new GetAllEmployeesQuery(), default);

        result.Should().HaveCount(3);
        result.Count(e => e.Role == EmployeeRole.Manager).Should().Be(1);
        result.Count(e => e.IsActive).Should().Be(2);
    }

    [Fact]
    public async Task CreateEmployeeHandler_WithZeroSalary_ShouldSucceed()
    {
        var handler = new CreateEmployeeHandler(_unitOfWorkMock.Object);
        var command = new CreateEmployeeCommand
        {
            FirstName = "Test",
            LastName = "User",
            NationalCode = "000",
            Role = EmployeeRole.Cleaner,
            BaseSalary = 0,
            BranchId = Guid.NewGuid()
        };

        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Employee>(), default))
            .ReturnsAsync((Employee e, CancellationToken _) => e);

        var result = await handler.Handle(command, default);

        result.BaseSalary.Should().Be(0);
    }
}
