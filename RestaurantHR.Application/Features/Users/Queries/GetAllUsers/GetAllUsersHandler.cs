using MediatR;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Application.DTOs;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllUsersHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.Repository<User>().GetAllAsync(cancellationToken);
        var employees = (await _unitOfWork.Repository<Employee>().GetAllAsync(cancellationToken)).ToDictionary(e => e.Id);
        return users.Select(u => new UserDto
        {
            Id = u.Id,
            Username = u.Username,
            Role = u.Role,
            EmployeeId = u.EmployeeId,
            EmployeeName = u.EmployeeId.HasValue && employees.TryGetValue(u.EmployeeId.Value, out var emp) ? $"{emp.FirstName} {emp.LastName}" : null,
            IsActive = u.IsActive,
        }).ToList();
    }
}