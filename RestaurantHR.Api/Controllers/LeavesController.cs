using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantHR.Application.Features.Leaves.Commands.CreateLeave;
using RestaurantHR.Application.Features.Leaves.Commands.DeleteLeave;
using RestaurantHR.Application.Features.Leaves.Commands.UpdateLeave;
using RestaurantHR.Application.Features.Leaves.Queries.GetAllLeaves;
using RestaurantHR.Application.Features.Leaves.Queries.GetLeaveById;

namespace RestaurantHR.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LeavesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeavesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllLeavesQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetLeaveByIdQuery(id));
        if (result is null)
            return NotFound(new { Message = $"Leave with id {id} not found" });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLeaveCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLeaveCommand command)
    {
        if (id != command.Id)
            return BadRequest(new { Message = "Id mismatch" });
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteLeaveCommand(id));
        return NoContent();
    }
}
