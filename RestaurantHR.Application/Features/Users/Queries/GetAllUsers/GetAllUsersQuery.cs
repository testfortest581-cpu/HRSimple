using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Users.Queries.GetAllUsers;

public record GetAllUsersQuery : IRequest<List<UserDto>>;