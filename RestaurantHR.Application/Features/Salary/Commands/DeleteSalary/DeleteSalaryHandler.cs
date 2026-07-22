using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Salary.Commands.DeleteSalary;

public class DeleteSalaryHandler : IRequestHandler<DeleteSalaryCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSalaryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteSalaryCommand request, CancellationToken cancellationToken)
    {
        var salary = await _unitOfWork.Repository<Domain.Entities.Salary>().GetByIdAsync(request.Id, cancellationToken);
        if (salary is null)
            throw new KeyNotFoundException($"Salary with id {request.Id} not found");

        _unitOfWork.Repository<Domain.Entities.Salary>().Delete(salary);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
