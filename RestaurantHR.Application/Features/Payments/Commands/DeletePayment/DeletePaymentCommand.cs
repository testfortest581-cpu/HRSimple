using MediatR;

namespace RestaurantHR.Application.Features.Payments.Commands.DeletePayment;

public record DeletePaymentCommand(Guid Id) : IRequest;
