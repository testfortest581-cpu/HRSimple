using System.Security.Cryptography;
using System.Text;
using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Users.Commands.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existing = await _unitOfWork.Repository<User>().FindAsync(u => u.Username == request.Username, cancellationToken);
        if (existing.Any())
            throw new InvalidOperationException("Username already exists");

        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(request.Password));
        var hashStr = Convert.ToHexString(hash).ToLower();

        string? employeeName = null;
        if (request.EmployeeId.HasValue)
        {
            var emp = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.EmployeeId.Value, cancellationToken);
            if (emp is not null)
                employeeName = $"{emp.FirstName} {emp.LastName}";
        }

        var user = new User
        {
            Username = request.Username,
            PasswordHash = hashStr,
            Role = request.Role,
            EmployeeId = request.EmployeeId,
            AvatarUrl = request.AvatarUrl,
        };

        await _unitOfWork.Repository<User>().AddAsync(user, cancellationToken);
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