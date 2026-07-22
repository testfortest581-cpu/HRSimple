using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Shifts.Queries.GetShiftById;

public class GetShiftByIdHandler : IRequestHandler<GetShiftByIdQuery, ShiftDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetShiftByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ShiftDto?> Handle(GetShiftByIdQuery request, CancellationToken cancellationToken)
    {
        var shift = await _unitOfWork.Repository<Shift>().GetByIdAsync(request.Id, cancellationToken);
        if (shift is null) return null;

        return new ShiftDto
        {
            Id = shift.Id,
            Name = shift.Name,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            BranchId = shift.BranchId,
            IsActive = shift.IsActive
        };
    }
}
