using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.WeeklyAssignments.Commands.UpdateWeeklyAssignment;

public class UpdateWeeklyAssignmentHandler : IRequestHandler<UpdateWeeklyAssignmentCommand, WeeklyAssignmentDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateWeeklyAssignmentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<WeeklyAssignmentDto> Handle(UpdateWeeklyAssignmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<WeeklyAssignment>().GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
            throw new KeyNotFoundException($"WeeklyAssignment with id {request.Id} not found");

        var employees = (await _unitOfWork.Repository<Employee>().GetAllAsync(cancellationToken)).ToDictionary(e => e.Id);
        var shifts = (await _unitOfWork.Repository<Shift>().GetAllAsync(cancellationToken)).ToDictionary(s => s.Id);
        var branches = (await _unitOfWork.Repository<Branch>().GetAllAsync(cancellationToken)).ToDictionary(b => b.Id);

        if (request.EmployeeId != Guid.Empty) entity.EmployeeId = request.EmployeeId;
        if (request.BranchId != Guid.Empty) entity.BranchId = request.BranchId;
        if (request.ShiftId != Guid.Empty) entity.ShiftId = request.ShiftId;
        if (request.WorkDays != "[]") entity.WorkDays = request.WorkDays;
        if (request.WeekStartDate != default) entity.WeekStartDate = request.WeekStartDate;
        if (request.IsActive.HasValue) entity.IsActive = request.IsActive.Value;

        _unitOfWork.Repository<WeeklyAssignment>().Update(entity);
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
