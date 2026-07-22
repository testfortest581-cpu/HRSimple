using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Common.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user, string? employeeName = null);
}