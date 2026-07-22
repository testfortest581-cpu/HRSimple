using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Salary.Commands.CreateSalary;

public class CreateSalaryHandler : IRequestHandler<CreateSalaryCommand, SalaryDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSalaryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SalaryDto> Handle(CreateSalaryCommand request, CancellationToken cancellationToken)
    {
        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.EmployeeId, cancellationToken);
        if (employee is null)
            throw new KeyNotFoundException($"Employee with id {request.EmployeeId} not found");

        var salary = new Domain.Entities.Salary
        {
            EmployeeId = request.EmployeeId,
            Month = request.Month,
            Year = request.Year,
            BaseSalary = request.BaseSalary,
            Overtime = request.Overtime,
            Bonus = request.Bonus,
            Deductions = request.Deductions,
            IsPaid = request.IsPaid
        };

        await _unitOfWork.Repository<Domain.Entities.Salary>().AddAsync(salary, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new SalaryDto
        {
            Id = salary.Id,
            EmployeeId = salary.EmployeeId,
            EmployeeName = $"{employee.FirstName} {employee.LastName}",
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
