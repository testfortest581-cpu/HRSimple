using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Payments.Queries.GetPaymentById;

public class GetPaymentByIdHandler : IRequestHandler<GetPaymentByIdQuery, PaymentDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPaymentByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaymentDto?> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        var payment = await _unitOfWork.Repository<Payment>().GetByIdAsync(request.Id, cancellationToken);
        if (payment is null) return null;

        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(payment.EmployeeId, cancellationToken);

        return new PaymentDto
        {
            Id = payment.Id,
            EmployeeId = payment.EmployeeId,
            EmployeeName = employee is not null ? employee.FirstName + " " + employee.LastName : string.Empty,
            Amount = payment.Amount,
            PaymentDate = payment.PaymentDate,
            PaymentType = payment.PaymentType,
            Description = payment.Description,
            SalaryId = payment.SalaryId
        };
    }
}
