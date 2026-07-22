using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Application.Features.Auth.Commands.ChangePassword;
using RestaurantHR.Application.Features.Auth.Commands.Login;
using RestaurantHR.Application.Features.Users.Commands.ResetPassword;
using RestaurantHR.Infrastructure.Data;

namespace RestaurantHR.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly AppDbContext _context;

    public AuthController(IMediator mediator, AppDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var result = await _mediator.Send(new LoginCommand
            {
                Username = request.Username,
                Password = request.Password,
            });
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim is null) return Unauthorized();
        await _mediator.Send(new ChangePasswordCommand(Guid.Parse(userIdClaim), request.CurrentPassword, request.NewPassword));
        return Ok(new { message = "Password changed successfully" });
    }

    [HttpPost("{id:guid}/reset-password")]
    public async Task<IActionResult> ResetPassword(Guid id, [FromBody] ResetPasswordRequest request)
    {
        await _mediator.Send(new ResetPasswordCommand(id, request.NewPassword));
        return Ok(new { message = "Password reset successfully" });
    }

    [HttpPost("upload-avatar/{userId}")]
    public async Task<IActionResult> UploadAvatar(int userId, IFormFile file)
    {
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(ext))
            return BadRequest("Invalid file type. Allowed: jpg, png, gif, webp");

        var dir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "avatars");
        Directory.CreateDirectory(dir);

        var fileName = $"user_{userId}{ext}";
        var filePath = Path.Combine(dir, fileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
            await file.CopyToAsync(stream);

        var urlPath = $"/uploads/avatars/{fileName}";

        var user = await _context.Set<Domain.Entities.User>().FindAsync(userId);
        if (user is not null)
        {
            user.AvatarUrl = urlPath;
            await _context.SaveChangesAsync();
        }

        return Ok(urlPath);
    }
}