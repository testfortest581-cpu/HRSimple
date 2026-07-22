using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Domain.Entities;
using AttendanceEntity = RestaurantHR.Domain.Entities.Attendance;

namespace RestaurantHR.Application.Features.Attendance.Commands.DeleteAttendance;

public class DeleteAttendanceHandler : IRequestHandler<DeleteAttendanceCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAttendanceHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteAttendanceCommand request, CancellationToken cancellationToken)
    {
        var attendance = await _unitOfWork.Repository<AttendanceEntity>().GetByIdAsync(request.Id, cancellationToken);
        if (attendance is null)
            throw new KeyNotFoundException($"Attendance with id {request.Id} not found");

        _unitOfWork.Repository<AttendanceEntity>().Delete(attendance);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
