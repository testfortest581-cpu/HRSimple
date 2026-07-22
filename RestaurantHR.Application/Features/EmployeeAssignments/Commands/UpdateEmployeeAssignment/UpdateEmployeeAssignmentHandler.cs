using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.EmployeeAssignments.Commands.UpdateEmployeeAssignment;

public class UpdateEmployeeAssignmentHandler : IRequestHandler<UpdateEmployeeAssignmentCommand, EmployeeAssignmentDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEmployeeAssignmentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<EmployeeAssignmentDto> Handle(UpdateEmployeeAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = await _unitOfWork.Repository<EmployeeAssignment>().GetByIdAsync(request.Id, cancellationToken);
        if (assignment is null)
            throw new KeyNotFoundException($"EmployeeAssignment with id {request.Id} not found");

        var employees = (await _unitOfWork.Repository<Employee>().GetAllAsync(cancellationToken)).ToDictionary(e => e.Id);
        var shifts = (await _unitOfWork.Repository<Shift>().GetAllAsync(cancellationToken)).ToDictionary(s => s.Id);
        var branches = (await _unitOfWork.Repository<Branch>().GetAllAsync(cancellationToken)).ToDictionary(b => b.Id);

        assignment.EmployeeId = request.EmployeeId;
        assignment.ShiftId = request.ShiftId;
        assignment.BranchId = request.BranchId;
        assignment.WorkDays = request.WorkDays;
        assignment.StartDate = request.StartDate;
        assignment.EndDate = request.EndDate;
        assignment.IsActive = request.IsActive;

        _unitOfWork.Repository<EmployeeAssignment>().Update(assignment);
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
