using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantHR.Application.Features.Salary.Commands.CreateSalary;
using RestaurantHR.Application.Features.Salary.Commands.DeleteSalary;
using RestaurantHR.Application.Features.Salary.Commands.UpdateSalary;
using RestaurantHR.Application.Features.Salary.Queries.GetAllSalaries;
using RestaurantHR.Application.Features.Salary.Queries.GetSalaryById;

namespace RestaurantHR.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SalariesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SalariesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllSalariesQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetSalaryByIdQuery(id));
        if (result is null)
            return NotFound(new { Message = $"Salary with id {id} not found" });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSalaryCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSalaryCommand command)
    {
        if (id != command.Id)
            return BadRequest(new { Message = "Id mismatch" });
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteSalaryCommand(id));
        return NoContent();
    }
}
