using MediatR;

namespace RestaurantHR.Application.Features.Users.Commands.ResetPassword;

public record ResetPasswordCommand(Guid UserId, string NewPassword) : IRequest;
