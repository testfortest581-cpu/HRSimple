using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Employees.Commands.CreateEmployee;

public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmployeeHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            NationalCode = request.NationalCode,
            Phone = request.Phone,
            Email = request.Email,
            Role = request.Role,
            HireDate = request.HireDate,
            BaseSalary = request.BaseSalary,
            BranchId = request.BranchId
        };

        await _unitOfWork.Repository<Employee>().AddAsync(employee, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new EmployeeDto
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            NationalCode = employee.NationalCode,
            Phone = employee.Phone,
            Email = employee.Email,
            Role = employee.Role,
            HireDate = employee.HireDate,
            BaseSalary = employee.BaseSalary,
            IsActive = employee.IsActive,
            BranchId = employee.BranchId
        };
    }
}
