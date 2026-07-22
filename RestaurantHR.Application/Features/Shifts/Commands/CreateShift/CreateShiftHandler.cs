using AutoMapper;
using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Shifts.Commands.CreateShift;

public class CreateShiftHandler : IRequestHandler<CreateShiftCommand, ShiftDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateShiftHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ShiftDto> Handle(CreateShiftCommand request, CancellationToken cancellationToken)
    {
        var shift = new Shift
        {
            Name = request.Name,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            BranchId = request.BranchId
        };

        await _unitOfWork.Repository<Shift>().AddAsync(shift, cancellationToken);
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
