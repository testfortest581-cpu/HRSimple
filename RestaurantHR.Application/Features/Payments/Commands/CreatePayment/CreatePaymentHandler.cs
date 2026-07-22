using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Payments.Commands.CreatePayment;

public class CreatePaymentHandler : IRequestHandler<CreatePaymentCommand, PaymentDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePaymentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaymentDto> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.EmployeeId, cancellationToken);

        var payment = new Payment
        {
            EmployeeId = request.EmployeeId,
            Amount = request.Amount,
            PaymentDate = request.PaymentDate,
            PaymentType = request.PaymentType,
            Description = request.Description,
            SalaryId = request.SalaryId
        };

        await _unitOfWork.Repository<Payment>().AddAsync(payment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

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
