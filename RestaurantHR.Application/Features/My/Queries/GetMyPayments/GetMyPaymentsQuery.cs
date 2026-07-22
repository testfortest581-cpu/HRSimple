using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.My.Queries.GetMyPayments;

public record GetMyPaymentsQuery(Guid EmployeeId) : IRequest<List<MyPaymentDto>>;