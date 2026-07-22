using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.WeeklyAssignments.Commands.CreateWeeklyAssignment;

public class CreateWeeklyAssignmentHandler : IRequestHandler<CreateWeeklyAssignmentCommand, WeeklyAssignmentDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateWeeklyAssignmentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<WeeklyAssignmentDto> Handle(CreateWeeklyAssignmentCommand request, CancellationToken cancellationToken)
    {
        var employees = (await _unitOfWork.Repository<Employee>().GetAllAsync(cancellationToken)).ToDictionary(e => e.Id);
        var shifts = (await _unitOfWork.Repository<Shift>().GetAllAsync(cancellationToken)).ToDictionary(s => s.Id);
        var branches = (await _unitOfWork.Repository<Branch>().GetAllAsync(cancellationToken)).ToDictionary(b => b.Id);

        var entity = new WeeklyAssignment
        {
            EmployeeId = request.EmployeeId,
            BranchId = request.BranchId,
            ShiftId = request.ShiftId,
            WorkDays = request.WorkDays,
            WeekStartDate = request.WeekStartDate,
        };

        await _unitOfWork.Repository<WeeklyAssignment>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

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
