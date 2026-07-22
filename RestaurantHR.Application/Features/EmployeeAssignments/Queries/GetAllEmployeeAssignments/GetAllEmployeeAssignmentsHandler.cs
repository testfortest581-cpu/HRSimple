using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.EmployeeAssignments.Queries.GetAllEmployeeAssignments;

public class GetAllEmployeeAssignmentsHandler : IRequestHandler<GetAllEmployeeAssignmentsQuery, List<EmployeeAssignmentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllEmployeeAssignmentsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<EmployeeAssignmentDto>> Handle(GetAllEmployeeAssignmentsQuery request, CancellationToken cancellationToken)
    {
        var employees = (await _unitOfWork.Repository<Employee>().GetAllAsync(cancellationToken)).ToDictionary(e => e.Id);
        var shifts = (await _unitOfWork.Repository<Shift>().GetAllAsync(cancellationToken)).ToDictionary(s => s.Id);
        var branches = (await _unitOfWork.Repository<Branch>().GetAllAsync(cancellationToken)).ToDictionary(b => b.Id);

        var assignments = await _unitOfWork.Repository<EmployeeAssignment>().FindAsync(a => a.IsActive, cancellationToken);
        return assignments.Select(a => new EmployeeAssignmentDto
        {
            Id = a.Id,
            EmployeeId = a.EmployeeId,
            EmployeeName = employees.TryGetValue(a.EmployeeId, out var emp) ? $"{emp.FirstName} {emp.LastName}" : string.Empty,
            ShiftId = a.ShiftId,
            ShiftName = shifts.TryGetValue(a.ShiftId, out var shift) ? shift.Name : string.Empty,
            WorkDays = a.WorkDays,
            BranchId = a.BranchId,
            BranchName = branches.TryGetValue(a.BranchId, out var branch) ? branch.Name : string.Empty,
            StartDate = a.StartDate,
            EndDate = a.EndDate,
            IsActive = a.IsActive
        }).ToList();
    }
}
