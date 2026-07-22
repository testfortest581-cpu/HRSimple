using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEmployeeHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<EmployeeDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.Id, cancellationToken);
        if (employee is null)
            throw new KeyNotFoundException($"Employee with id {request.Id} not found");

        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.NationalCode = request.NationalCode;
        employee.Phone = request.Phone;
        employee.Email = request.Email;
        employee.Role = request.Role;
        employee.BaseSalary = request.BaseSalary;
        employee.BranchId = request.BranchId;

        _unitOfWork.Repository<Employee>().Update(employee);
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
            BranchId = employee.BranchId ?? Guid.Empty
        };
    }
}
