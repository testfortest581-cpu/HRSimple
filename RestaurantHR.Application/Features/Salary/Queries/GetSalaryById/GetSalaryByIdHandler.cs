using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Salary.Queries.GetSalaryById;

public class GetSalaryByIdHandler : IRequestHandler<GetSalaryByIdQuery, SalaryDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSalaryByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SalaryDto?> Handle(GetSalaryByIdQuery request, CancellationToken cancellationToken)
    {
        var salary = await _unitOfWork.Repository<Domain.Entities.Salary>().GetByIdAsync(request.Id, cancellationToken);
        if (salary is null) return null;

        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(salary.EmployeeId, cancellationToken);

        return new SalaryDto
        {
            Id = salary.Id,
            EmployeeId = salary.EmployeeId,
            EmployeeName = employee is not null ? $"{employee.FirstName} {employee.LastName}" : "Unknown",
            Month = salary.Month,
            Year = salary.Year,
            BaseSalary = salary.BaseSalary,
            Overtime = salary.Overtime,
            Bonus = salary.Bonus,
            Deductions = salary.Deductions,
            NetSalary = salary.NetSalary,
            IsPaid = salary.IsPaid,
            PaidAt = salary.PaidAt
        };
    }
}
