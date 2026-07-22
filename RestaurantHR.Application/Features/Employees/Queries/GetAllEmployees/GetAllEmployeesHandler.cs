using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Employees.Queries.GetAllEmployees;

public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesQuery, List<EmployeeDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllEmployeesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        var employees = await _unitOfWork.Repository<Employee>().FindAsync(e => e.IsActive, cancellationToken);
        return employees.Select(e => new EmployeeDto
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            NationalCode = e.NationalCode,
            Phone = e.Phone,
            Email = e.Email,
            Role = e.Role,
            HireDate = e.HireDate,
            BaseSalary = e.BaseSalary,
            IsActive = e.IsActive,
            BranchId = e.BranchId
        }).ToList();
    }
}
