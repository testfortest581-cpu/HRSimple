using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Shifts.Queries.GetAllShifts;

public class GetAllShiftsHandler : IRequestHandler<GetAllShiftsQuery, List<ShiftDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllShiftsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ShiftDto>> Handle(GetAllShiftsQuery request, CancellationToken cancellationToken)
    {
        var shifts = await _unitOfWork.Repository<Shift>().FindAsync(s => s.IsActive, cancellationToken);
        return shifts.Select(s => new ShiftDto
        {
            Id = s.Id,
            Name = s.Name,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            BranchId = s.BranchId,
            IsActive = s.IsActive
        }).ToList();
    }
}
