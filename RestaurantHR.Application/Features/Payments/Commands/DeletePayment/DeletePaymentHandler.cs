using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Payments.Commands.DeletePayment;

public class DeletePaymentHandler : IRequestHandler<DeletePaymentCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePaymentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await _unitOfWork.Repository<Payment>().GetByIdAsync(request.Id, cancellationToken);
        if (payment is null)
            throw new KeyNotFoundException($"Payment with id {request.Id} not found");

        _unitOfWork.Repository<Payment>().Delete(payment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
