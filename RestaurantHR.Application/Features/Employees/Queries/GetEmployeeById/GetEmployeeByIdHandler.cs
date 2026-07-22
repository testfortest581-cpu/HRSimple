using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetEmployeeByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<EmployeeDto?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.Id, cancellationToken);
        if (employee is null) return null;

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
