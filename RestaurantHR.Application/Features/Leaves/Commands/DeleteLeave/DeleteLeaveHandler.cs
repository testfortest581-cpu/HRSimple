using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Leaves.Commands.DeleteLeave;

public class DeleteLeaveHandler : IRequestHandler<DeleteLeaveCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLeaveHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteLeaveCommand request, CancellationToken cancellationToken)
    {
        var leave = await _unitOfWork.Repository<Leave>().GetByIdAsync(request.Id, cancellationToken);
        if (leave is null)
            throw new KeyNotFoundException($"Leave with id {request.Id} not found");

        _unitOfWork.Repository<Leave>().Delete(leave);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
