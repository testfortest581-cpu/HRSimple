using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Shifts.Commands.DeleteShift;

public class DeleteShiftHandler : IRequestHandler<DeleteShiftCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteShiftHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteShiftCommand request, CancellationToken cancellationToken)
    {
        var shift = await _unitOfWork.Repository<Shift>().GetByIdAsync(request.Id, cancellationToken);
        if (shift is null)
            throw new KeyNotFoundException($"Shift with id {request.Id} not found");

        shift.IsActive = false;
        _unitOfWork.Repository<Shift>().Update(shift);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
