using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.EmployeeAssignments.Queries.GetEmployeeAssignmentById;

public class GetEmployeeAssignmentByIdHandler : IRequestHandler<GetEmployeeAssignmentByIdQuery, EmployeeAssignmentDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetEmployeeAssignmentByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<EmployeeAssignmentDto?> Handle(GetEmployeeAssignmentByIdQuery request, CancellationToken cancellationToken)
    {
        var assignment = await _unitOfWork.Repository<EmployeeAssignment>().GetByIdAsync(request.Id, cancellationToken);
        if (assignment is null) return null;

        var employees = (await _unitOfWork.Repository<Employee>().GetAllAsync(cancellationToken)).ToDictionary(e => e.Id);
        var shifts = (await _unitOfWork.Repository<Shift>().GetAllAsync(cancellationToken)).ToDictionary(s => s.Id);
        var branches = (await _unitOfWork.Repository<Branch>().GetAllAsync(cancellationToken)).ToDictionary(b => b.Id);

        return new EmployeeAssignmentDto
        {
            Id = assignment.Id,
            EmployeeId = assignment.EmployeeId,
            EmployeeName = employees.TryGetValue(assignment.EmployeeId, out var emp) ? $"{emp.FirstName} {emp.LastName}" : string.Empty,
            ShiftId = assignment.ShiftId,
            ShiftName = shifts.TryGetValue(assignment.ShiftId, out var shift) ? shift.Name : string.Empty,
            BranchId = assignment.BranchId,
            BranchName = branches.TryGetValue(assignment.BranchId, out var branch) ? branch.Name : string.Empty,
            StartDate = assignment.StartDate,
            EndDate = assignment.EndDate,
            IsActive = assignment.IsActive
        };
    }
}
