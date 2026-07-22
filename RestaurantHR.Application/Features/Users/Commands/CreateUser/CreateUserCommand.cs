using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand : IRequest<UserDto>
{
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Role { get; init; } = "Employee";
    public Guid? EmployeeId { get; init; }
    public string? AvatarUrl { get; init; }
}