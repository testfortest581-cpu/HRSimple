using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantHR.Application.Features.Employees.Commands.CreateEmployee;
using RestaurantHR.Application.Features.Employees.Commands.UpdateEmployee;
using RestaurantHR.Application.Features.Employees.Queries.GetAllEmployees;
using RestaurantHR.Application.Features.Employees.Commands.DeleteEmployee;
using RestaurantHR.Application.Features.Employees.Queries.GetEmployeeById;

namespace RestaurantHR.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllEmployeesQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetEmployeeByIdQuery(id));
        if (result is null)
            return NotFound(new { Message = $"Employee with id {id} not found" });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmployeeCommand command)
    {
        if (id != command.Id)
            return BadRequest(new { Message = "Id mismatch" });
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteEmployeeCommand(id));
        return NoContent();
    }
}
