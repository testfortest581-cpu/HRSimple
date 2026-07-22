using MediatR;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Enums;

namespace RestaurantHR.Application.Features.Payments.Commands.UpdatePayment;

public record UpdatePaymentCommand : IRequest<PaymentDto>
{
    public Guid Id { get; init; }
    public Guid EmployeeId { get; init; }
    public decimal Amount { get; init; }
    public DateTime PaymentDate { get; init; }
    public PaymentType PaymentType { get; init; }
    public string Description { get; init; } = string.Empty;
    public Guid? SalaryId { get; init; }
}
