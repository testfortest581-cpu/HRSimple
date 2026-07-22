using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.My.Queries.GetMyPayments;

public class GetMyPaymentsHandler : IRequestHandler<GetMyPaymentsQuery, List<MyPaymentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMyPaymentsHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<List<MyPaymentDto>> Handle(GetMyPaymentsQuery request, CancellationToken cancellationToken)
    {
        var payments = await _unitOfWork.Repository<Payment>().FindAsync(p => p.EmployeeId == request.EmployeeId, cancellationToken);
        return payments.OrderByDescending(p => p.PaymentDate).Select(p => new MyPaymentDto
        {
            Id = p.Id,
            Amount = p.Amount,
            PaymentDate = p.PaymentDate.ToString("yyyy-MM-dd"),
            PaymentType = p.PaymentType.ToString(),
            Description = p.Description,
        }).ToList();
    }
}