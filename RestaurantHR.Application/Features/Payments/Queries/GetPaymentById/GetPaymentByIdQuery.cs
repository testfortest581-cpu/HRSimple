using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Payments.Queries.GetPaymentById;

public record GetPaymentByIdQuery(Guid Id) : IRequest<PaymentDto?>;
