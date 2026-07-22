using System.Security.Cryptography;
using System.Text;
using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Auth.Commands.ChangePassword;

public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public ChangePasswordHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
            throw new KeyNotFoundException("User not found");

        var currentHash = SHA256.HashData(Encoding.UTF8.GetBytes(request.CurrentPassword));
        var currentHashStr = Convert.ToHexString(currentHash).ToLower();

        if (user.PasswordHash != currentHashStr)
            throw new UnauthorizedAccessException("Current password is incorrect");

        var newHash = SHA256.HashData(Encoding.UTF8.GetBytes(request.NewPassword));
        user.PasswordHash = Convert.ToHexString(newHash).ToLower();

        _unitOfWork.Repository<User>().Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
