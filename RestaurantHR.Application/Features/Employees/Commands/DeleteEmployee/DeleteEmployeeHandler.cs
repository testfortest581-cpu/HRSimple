using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEmployeeHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.Id, cancellationToken);
        if (employee is null)
            throw new KeyNotFoundException($"Employee with id {request.Id} not found");

        employee.IsActive = false;
        _unitOfWork.Repository<Employee>().Update(employee);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
