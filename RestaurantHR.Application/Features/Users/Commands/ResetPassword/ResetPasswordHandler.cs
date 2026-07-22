using System.Security.Cryptography;
using System.Text;
using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Users.Commands.ResetPassword;

public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public ResetPasswordHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
            throw new KeyNotFoundException("User not found");

        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(request.NewPassword));
        user.PasswordHash = Convert.ToHexString(hash).ToLower();

        _unitOfWork.Repository<User>().Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
