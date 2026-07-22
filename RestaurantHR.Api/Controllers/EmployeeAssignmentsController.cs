using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantHR.Application.Features.EmployeeAssignments.Commands.CreateEmployeeAssignment;
using RestaurantHR.Application.Features.EmployeeAssignments.Commands.DeleteEmployeeAssignment;
using RestaurantHR.Application.Features.EmployeeAssignments.Commands.UpdateEmployeeAssignment;
using RestaurantHR.Application.Features.EmployeeAssignments.Queries.GetAllEmployeeAssignments;
using RestaurantHR.Application.Features.EmployeeAssignments.Queries.GetEmployeeAssignmentById;

namespace RestaurantHR.Api.Controllers;

[ApiController]
[Route("api/employee-assignments")]
[Authorize]
public class EmployeeAssignmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeAssignmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllEmployeeAssignmentsQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetEmployeeAssignmentByIdQuery(id));
        if (result is null)
            return NotFound(new { Message = $"EmployeeAssignment with id {id} not found" });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeAssignmentCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmployeeAssignmentCommand command)
    {
        if (id != command.Id)
            return BadRequest(new { Message = "Id mismatch" });
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteEmployeeAssignmentCommand(id));
        return NoContent();
    }
}
