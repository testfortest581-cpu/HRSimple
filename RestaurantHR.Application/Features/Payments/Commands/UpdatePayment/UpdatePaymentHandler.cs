using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Payments.Commands.UpdatePayment;

public class UpdatePaymentHandler : IRequestHandler<UpdatePaymentCommand, PaymentDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePaymentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaymentDto> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await _unitOfWork.Repository<Payment>().GetByIdAsync(request.Id, cancellationToken);
        if (payment is null)
            throw new KeyNotFoundException($"Payment with id {request.Id} not found");

        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.EmployeeId, cancellationToken);

        payment.EmployeeId = request.EmployeeId;
        payment.Amount = request.Amount;
        payment.PaymentDate = request.PaymentDate;
        payment.PaymentType = request.PaymentType;
        payment.Description = request.Description;
        payment.SalaryId = request.SalaryId;

        _unitOfWork.Repository<Payment>().Update(payment);
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
