using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.EmployeeAssignments.Commands.CreateEmployeeAssignment;

public class CreateEmployeeAssignmentHandler : IRequestHandler<CreateEmployeeAssignmentCommand, EmployeeAssignmentDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmployeeAssignmentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<EmployeeAssignmentDto> Handle(CreateEmployeeAssignmentCommand request, CancellationToken cancellationToken)
    {
        var employees = (await _unitOfWork.Repository<Employee>().GetAllAsync(cancellationToken)).ToDictionary(e => e.Id);
        var shifts = (await _unitOfWork.Repository<Shift>().GetAllAsync(cancellationToken)).ToDictionary(s => s.Id);
        var branches = (await _unitOfWork.Repository<Branch>().GetAllAsync(cancellationToken)).ToDictionary(b => b.Id);

        var assignment = new EmployeeAssignment
        {
            EmployeeId = request.EmployeeId,
            ShiftId = request.ShiftId,
            BranchId = request.BranchId,
            WorkDays = request.WorkDays,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };

        await _unitOfWork.Repository<EmployeeAssignment>().AddAsync(assignment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new EmployeeAssignmentDto
        {
            Id = assignment.Id,
            EmployeeId = assignment.EmployeeId,
            EmployeeName = employees.TryGetValue(assignment.EmployeeId, out var emp) ? $"{emp.FirstName} {emp.LastName}" : string.Empty,
            ShiftId = assignment.ShiftId,
            ShiftName = shifts.TryGetValue(assignment.ShiftId, out var shift) ? shift.Name : string.Empty,
            WorkDays = assignment.WorkDays,
            BranchId = assignment.BranchId,
            BranchName = branches.TryGetValue(assignment.BranchId, out var branch) ? branch.Name : string.Empty,
            StartDate = assignment.StartDate,
            EndDate = assignment.EndDate,
            IsActive = assignment.IsActive
        };
    }
}
