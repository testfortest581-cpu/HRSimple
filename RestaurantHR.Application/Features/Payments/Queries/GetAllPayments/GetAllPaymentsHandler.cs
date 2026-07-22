using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Payments.Queries.GetAllPayments;

public class GetAllPaymentsHandler : IRequestHandler<GetAllPaymentsQuery, List<PaymentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPaymentsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<PaymentDto>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
    {
        var payments = await _unitOfWork.Repository<Payment>().GetAllAsync(cancellationToken);
        var employees = await _unitOfWork.Repository<Employee>().GetAllAsync(cancellationToken);
        var employeeDict = employees.ToDictionary(e => e.Id, e => e.FirstName + " " + e.LastName);

        return payments.Select(p => new PaymentDto
        {
            Id = p.Id,
            EmployeeId = p.EmployeeId,
            EmployeeName = employeeDict.TryGetValue(p.EmployeeId, out var name) ? name : string.Empty,
            Amount = p.Amount,
            PaymentDate = p.PaymentDate,
            PaymentType = p.PaymentType,
            Description = p.Description,
            SalaryId = p.SalaryId
        }).ToList();
    }
}
