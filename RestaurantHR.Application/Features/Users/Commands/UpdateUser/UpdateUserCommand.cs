using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Users.Commands.UpdateUser;

public record UpdateUserCommand : IRequest<UserDto>
{
    public Guid Id { get; init; }
    public string? Username { get; init; }
    public string? Password { get; init; }
    public string? Role { get; init; }
    public Guid? EmployeeId { get; init; }
    public bool? IsActive { get; init; }
    public string? AvatarUrl { get; init; }
}
