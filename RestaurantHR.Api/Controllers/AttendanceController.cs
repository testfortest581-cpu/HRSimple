using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantHR.Application.Features.Attendance.Commands.CreateAttendance;
using RestaurantHR.Application.Features.Attendance.Commands.DeleteAttendance;
using RestaurantHR.Application.Features.Attendance.Commands.UpdateAttendance;
using RestaurantHR.Application.Features.Attendance.Queries.GetAllAttendances;
using RestaurantHR.Application.Features.Attendance.Queries.GetAttendanceById;

namespace RestaurantHR.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AttendanceController : ControllerBase
{
    private readonly IMediator _mediator;

    public AttendanceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllAttendancesQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetAttendanceByIdQuery(id));
        if (result is null)
            return NotFound(new { Message = $"Attendance with id {id} not found" });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAttendanceCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAttendanceCommand command)
    {
        if (id != command.Id)
            return BadRequest(new { Message = "Id mismatch" });
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteAttendanceCommand(id));
        return NoContent();
    }
}
