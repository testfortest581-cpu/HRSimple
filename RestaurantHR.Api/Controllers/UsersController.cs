using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Application.Features.Users.Commands.CreateUser;
using RestaurantHR.Application.Features.Users.Commands.DeleteUser;
using RestaurantHR.Application.Features.Users.Commands.UpdateUser;
using RestaurantHR.Application.Features.Users.Queries.GetAllUsers;

namespace RestaurantHR.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllUsersQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        var result = await _mediator.Send(new CreateUserCommand
        {
            Username = request.Username,
            Password = request.Password,
            Role = request.Role,
            EmployeeId = request.EmployeeId,
            AvatarUrl = request.AvatarUrl,
        });
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest request)
    {
        var result = await _mediator.Send(new UpdateUserCommand
        {
            Id = id,
            Username = request.Username,
            Password = request.Password,
            Role = request.Role,
            EmployeeId = request.EmployeeId,
            IsActive = request.IsActive,
            AvatarUrl = request.AvatarUrl,
        });
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteUserCommand(id));
        return NoContent();
    }
}