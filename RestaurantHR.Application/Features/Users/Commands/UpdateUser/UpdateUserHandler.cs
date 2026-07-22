using System.Security.Cryptography;
using System.Text;
using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.Id, cancellationToken);
        if (user is null)
            throw new KeyNotFoundException($"User with id {request.Id} not found");

        if (request.Username is not null && request.Username != user.Username)
        {
            var existing = await _unitOfWork.Repository<User>().FindAsync(u => u.Username == request.Username, cancellationToken);
            if (existing.Any(e => e.Id != request.Id))
                throw new InvalidOperationException("Username already exists");
            user.Username = request.Username;
        }

        if (request.Password is not null)
        {
            var hash = SHA256.HashData(Encoding.UTF8.GetBytes(request.Password));
            user.PasswordHash = Convert.ToHexString(hash).ToLower();
        }

        if (request.Role is not null)
            user.Role = request.Role;

        if (request.EmployeeId is not null)
            user.EmployeeId = request.EmployeeId == Guid.Empty ? null : request.EmployeeId;

        if (request.IsActive.HasValue)
            user.IsActive = request.IsActive.Value;

        if (request.AvatarUrl is not null)
            user.AvatarUrl = request.AvatarUrl == "" ? null : request.AvatarUrl;

        string? employeeName = null;
        if (user.EmployeeId.HasValue)
        {
            var emp = await _unitOfWork.Repository<Employee>().GetByIdAsync(user.EmployeeId.Value, cancellationToken);
            if (emp is not null)
                employeeName = $"{emp.FirstName} {emp.LastName}";
        }

        _unitOfWork.Repository<User>().Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Role = user.Role,
            EmployeeId = user.EmployeeId,
            EmployeeName = employeeName,
            AvatarUrl = user.AvatarUrl,
            IsActive = user.IsActive,
        };
    }
}
