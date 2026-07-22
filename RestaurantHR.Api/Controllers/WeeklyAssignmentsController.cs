using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantHR.Application.Features.WeeklyAssignments.Commands.CreateWeeklyAssignment;
using RestaurantHR.Application.Features.WeeklyAssignments.Commands.DeleteWeeklyAssignment;
using RestaurantHR.Application.Features.WeeklyAssignments.Commands.UpdateWeeklyAssignment;
using RestaurantHR.Application.Features.WeeklyAssignments.Queries.GetAllWeeklyAssignments;
using RestaurantHR.Application.Features.WeeklyAssignments.Queries.GetWeeklyAssignmentById;

namespace RestaurantHR.Api.Controllers;

[ApiController]
[Route("api/weekly-assignments")]
[Authorize]
public class WeeklyAssignmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public WeeklyAssignmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllWeeklyAssignmentsQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetWeeklyAssignmentByIdQuery(id));
        if (result is null)
            return NotFound(new { Message = $"WeeklyAssignment with id {id} not found" });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateWeeklyAssignmentCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWeeklyAssignmentCommand command)
    {
        if (id != command.Id)
            return BadRequest(new { Message = "Id mismatch" });
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteWeeklyAssignmentCommand(id));
        return NoContent();
    }
}
