using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.WeeklyAssignments.Commands.DeleteWeeklyAssignment;

public class DeleteWeeklyAssignmentHandler : IRequestHandler<DeleteWeeklyAssignmentCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteWeeklyAssignmentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteWeeklyAssignmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<WeeklyAssignment>().GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
            throw new KeyNotFoundException($"WeeklyAssignment with id {request.Id} not found");

        entity.IsActive = false;
        _unitOfWork.Repository<WeeklyAssignment>().Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
