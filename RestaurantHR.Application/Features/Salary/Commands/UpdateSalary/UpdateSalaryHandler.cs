using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Salary.Commands.UpdateSalary;

public class UpdateSalaryHandler : IRequestHandler<UpdateSalaryCommand, SalaryDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSalaryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SalaryDto> Handle(UpdateSalaryCommand request, CancellationToken cancellationToken)
    {
        var salary = await _unitOfWork.Repository<Domain.Entities.Salary>().GetByIdAsync(request.Id, cancellationToken);
        if (salary is null)
            throw new KeyNotFoundException($"Salary with id {request.Id} not found");

        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.EmployeeId, cancellationToken);
        if (employee is null)
            throw new KeyNotFoundException($"Employee with id {request.EmployeeId} not found");

        salary.EmployeeId = request.EmployeeId;
        salary.Month = request.Month;
        salary.Year = request.Year;
        salary.BaseSalary = request.BaseSalary;
        salary.Overtime = request.Overtime;
        salary.Bonus = request.Bonus;
        salary.Deductions = request.Deductions;
        salary.IsPaid = request.IsPaid;
        salary.PaidAt = request.PaidAt;

        _unitOfWork.Repository<Domain.Entities.Salary>().Update(salary);
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
