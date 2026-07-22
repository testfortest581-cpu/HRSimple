using System.Security.Cryptography;
using System.Text;
using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Auth.Commands.Login;

public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public LoginHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.Repository<User>().FindAsync(u => u.Username == request.Username && u.IsActive, cancellationToken);
        var user = users.FirstOrDefault();
        if (user is null)
            throw new UnauthorizedAccessException("Invalid username or password");

        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(request.Password));
        var hashStr = Convert.ToHexString(hash).ToLower();
        if (user.PasswordHash != hashStr)
            throw new UnauthorizedAccessException("Invalid username or password");

        string? employeeName = null;
        if (user.EmployeeId.HasValue)
        {
            var emp = await _unitOfWork.Repository<Employee>().GetByIdAsync(user.EmployeeId.Value, cancellationToken);
            if (emp is not null)
                employeeName = $"{emp.FirstName} {emp.LastName}";
        }

        return new LoginResponse
        {
            Token = _tokenService.GenerateToken(user, employeeName),
            User = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role,
                EmployeeId = user.EmployeeId,
                EmployeeName = employeeName,
                IsActive = user.IsActive,
            },
        };
    }
}