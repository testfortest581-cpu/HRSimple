using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Payments.Queries.GetAllPayments;

public record GetAllPaymentsQuery : IRequest<List<PaymentDto>>;
