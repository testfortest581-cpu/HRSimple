using System.Text.Json;
using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.My.Queries.GetMyAssignments;

public class GetMyAssignmentsHandler : IRequestHandler<GetMyAssignmentsQuery, List<MyAssignmentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMyAssignmentsHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<List<MyAssignmentDto>> Handle(GetMyAssignmentsQuery request, CancellationToken cancellationToken)
    {
        var weekly = await _unitOfWork.Repository<WeeklyAssignment>().FindAsync(w => w.EmployeeId == request.EmployeeId && w.IsActive, cancellationToken);
        var branches = (await _unitOfWork.Repository<Branch>().GetAllAsync(cancellationToken)).ToDictionary(b => b.Id);
        var shifts = (await _unitOfWork.Repository<Shift>().GetAllAsync(cancellationToken)).ToDictionary(s => s.Id);

        return weekly.OrderBy(w => w.WeekStartDate).Select(w =>
        {
            string[] workDays = [];
            try { workDays = JsonSerializer.Deserialize<string[]>(w.WorkDays) ?? []; } catch { }
            return new MyAssignmentDto
            {
                Id = w.Id,
                BranchName = branches.TryGetValue(w.BranchId, out var b) ? b.Name : string.Empty,
                ShiftName = shifts.TryGetValue(w.ShiftId, out var s) ? s.Name : string.Empty,
                WorkDays = workDays,
                WeekStartDate = w.WeekStartDate.ToString("yyyy-MM-dd"),
            };
        }).ToList();
    }
}