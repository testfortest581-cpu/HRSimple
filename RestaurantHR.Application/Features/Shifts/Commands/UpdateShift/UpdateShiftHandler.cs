using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Shifts.Commands.UpdateShift;

public class UpdateShiftHandler : IRequestHandler<UpdateShiftCommand, ShiftDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateShiftHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ShiftDto> Handle(UpdateShiftCommand request, CancellationToken cancellationToken)
    {
        var shift = await _unitOfWork.Repository<Shift>().GetByIdAsync(request.Id, cancellationToken);
        if (shift is null)
            throw new KeyNotFoundException($"Shift with id {request.Id} not found");

        shift.Name = request.Name;
        shift.StartTime = request.StartTime;
        shift.EndTime = request.EndTime;
        shift.BranchId = request.BranchId;
        if (request.IsActive.HasValue)
            shift.IsActive = request.IsActive.Value;

        _unitOfWork.Repository<Shift>().Update(shift);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

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
