using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantHR.Application.Features.My.Queries.GetMyAssignments;
using RestaurantHR.Application.Features.My.Queries.GetMyLeaves;
using RestaurantHR.Application.Features.My.Queries.GetMyPayments;

namespace RestaurantHR.Api.Controllers;

[ApiController]
[Route("api/my")]
[Authorize(Roles = "Employee")]
public class MyController : ControllerBase
{
    private readonly IMediator _mediator;

    public MyController(IMediator mediator) => _mediator = mediator;

    private Guid GetEmployeeId()
    {
        var empId = User.FindFirstValue("EmployeeId");
        if (empId is null)
            throw new UnauthorizedAccessException("User is not linked to an employee");
        return Guid.Parse(empId);
    }

    [HttpGet("leaves")]
    public async Task<IActionResult> GetMyLeaves()
    {
        var result = await _mediator.Send(new GetMyLeavesQuery(GetEmployeeId()));
        return Ok(result);
    }

    [HttpPost("leaves")]
    public async Task<IActionResult> CreateMyLeave([FromBody] CreateMyLeaveRequest request)
    {
        var empId = GetEmployeeId();
        var cmd = new RestaurantHR.Application.Features.My.Commands.CreateMyLeave.CreateMyLeaveCommand
        {
            EmployeeId = empId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            LeaveType = request.LeaveType,
            Reason = request.Reason,
        };
        var result = await _mediator.Send(cmd);
        return Ok(result);
    }

    [HttpGet("payments")]
    public async Task<IActionResult> GetMyPayments()
    {
        var result = await _mediator.Send(new GetMyPaymentsQuery(GetEmployeeId()));
        return Ok(result);
    }

    [HttpGet("assignments")]
    public async Task<IActionResult> GetMyAssignments()
    {
        var result = await _mediator.Send(new GetMyAssignmentsQuery(GetEmployeeId()));
        return Ok(result);
    }
}

public class CreateMyLeaveRequest
{
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string LeaveType { get; set; } = "Annual";
    public string Reason { get; set; } = string.Empty;
}