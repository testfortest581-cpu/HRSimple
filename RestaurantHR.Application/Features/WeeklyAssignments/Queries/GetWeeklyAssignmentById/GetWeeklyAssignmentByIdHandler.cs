using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.WeeklyAssignments.Queries.GetWeeklyAssignmentById;

public class GetWeeklyAssignmentByIdHandler : IRequestHandler<GetWeeklyAssignmentByIdQuery, WeeklyAssignmentDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetWeeklyAssignmentByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<WeeklyAssignmentDto?> Handle(GetWeeklyAssignmentByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<WeeklyAssignment>().GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;

        var employees = (await _unitOfWork.Repository<Employee>().GetAllAsync(cancellationToken)).ToDictionary(e => e.Id);
        var shifts = (await _unitOfWork.Repository<Shift>().GetAllAsync(cancellationToken)).ToDictionary(s => s.Id);
        var branches = (await _unitOfWork.Repository<Branch>().GetAllAsync(cancellationToken)).ToDictionary(b => b.Id);

        return new WeeklyAssignmentDto
        {
            Id = entity.Id,
            EmployeeId = entity.EmployeeId,
            EmployeeName = employees.TryGetValue(entity.EmployeeId, out var emp) ? $"{emp.FirstName} {emp.LastName}" : string.Empty,
            BranchId = entity.BranchId,
            BranchName = branches.TryGetValue(entity.BranchId, out var branch) ? branch.Name : string.Empty,
            ShiftId = entity.ShiftId,
            ShiftName = shifts.TryGetValue(entity.ShiftId, out var shift) ? shift.Name : string.Empty,
            WorkDays = entity.WorkDays,
            WeekStartDate = entity.WeekStartDate,
            IsActive = entity.IsActive,
        };
    }
}
