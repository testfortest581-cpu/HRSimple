using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantHR.Application.Features.Shifts.Commands.CreateShift;
using RestaurantHR.Application.Features.Shifts.Commands.DeleteShift;
using RestaurantHR.Application.Features.Shifts.Commands.UpdateShift;
using RestaurantHR.Application.Features.Shifts.Queries.GetAllShifts;
using RestaurantHR.Application.Features.Shifts.Queries.GetShiftById;

namespace RestaurantHR.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ShiftsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShiftsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllShiftsQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetShiftByIdQuery(id));
        if (result is null)
            return NotFound(new { Message = $"Shift with id {id} not found" });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateShiftCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateShiftCommand command)
    {
        if (id != command.Id)
            return BadRequest(new { Message = "Id mismatch" });
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteShiftCommand(id));
        return NoContent();
    }
}
