using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.WeeklyAssignments.Queries.GetAllWeeklyAssignments;

public class GetAllWeeklyAssignmentsHandler : IRequestHandler<GetAllWeeklyAssignmentsQuery, List<WeeklyAssignmentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllWeeklyAssignmentsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<WeeklyAssignmentDto>> Handle(GetAllWeeklyAssignmentsQuery request, CancellationToken cancellationToken)
    {
        var employees = (await _unitOfWork.Repository<Employee>().GetAllAsync(cancellationToken)).ToDictionary(e => e.Id);
        var shifts = (await _unitOfWork.Repository<Shift>().GetAllAsync(cancellationToken)).ToDictionary(s => s.Id);
        var branches = (await _unitOfWork.Repository<Branch>().GetAllAsync(cancellationToken)).ToDictionary(b => b.Id);

        var items = await _unitOfWork.Repository<WeeklyAssignment>().FindAsync(a => a.IsActive, cancellationToken);
        return items.Select(a => new WeeklyAssignmentDto
        {
            Id = a.Id,
            EmployeeId = a.EmployeeId,
            EmployeeName = employees.TryGetValue(a.EmployeeId, out var emp) ? $"{emp.FirstName} {emp.LastName}" : string.Empty,
            BranchId = a.BranchId,
            BranchName = branches.TryGetValue(a.BranchId, out var branch) ? branch.Name : string.Empty,
            ShiftId = a.ShiftId,
            ShiftName = shifts.TryGetValue(a.ShiftId, out var shift) ? shift.Name : string.Empty,
            WorkDays = a.WorkDays,
            WeekStartDate = a.WeekStartDate,
            IsActive = a.IsActive,
        }).ToList();
    }
}
