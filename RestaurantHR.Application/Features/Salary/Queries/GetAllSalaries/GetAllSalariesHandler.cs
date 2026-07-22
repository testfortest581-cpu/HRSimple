using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Salary.Queries.GetAllSalaries;

public class GetAllSalariesHandler : IRequestHandler<GetAllSalariesQuery, List<SalaryDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllSalariesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<SalaryDto>> Handle(GetAllSalariesQuery request, CancellationToken cancellationToken)
    {
        var salaries = await _unitOfWork.Repository<Domain.Entities.Salary>().GetAllAsync(cancellationToken);
        var employees = await _unitOfWork.Repository<Employee>().GetAllAsync(cancellationToken);
        var employeeDict = employees.ToDictionary(e => e.Id, e => $"{e.FirstName} {e.LastName}");

        return salaries.Select(s => new SalaryDto
        {
            Id = s.Id,
            EmployeeId = s.EmployeeId,
            EmployeeName = employeeDict.GetValueOrDefault(s.EmployeeId, "Unknown"),
            Month = s.Month,
            Year = s.Year,
            BaseSalary = s.BaseSalary,
            Overtime = s.Overtime,
            Bonus = s.Bonus,
            Deductions = s.Deductions,
            NetSalary = s.NetSalary,
            IsPaid = s.IsPaid,
            PaidAt = s.PaidAt
        }).ToList();
    }
}
