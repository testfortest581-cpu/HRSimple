using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.EmployeeAssignments.Commands.DeleteEmployeeAssignment;

public class DeleteEmployeeAssignmentHandler : IRequestHandler<DeleteEmployeeAssignmentCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEmployeeAssignmentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteEmployeeAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = await _unitOfWork.Repository<EmployeeAssignment>().GetByIdAsync(request.Id, cancellationToken);
        if (assignment is null)
            throw new KeyNotFoundException($"EmployeeAssignment with id {request.Id} not found");

        assignment.IsActive = false;
        _unitOfWork.Repository<EmployeeAssignment>().Update(assignment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
