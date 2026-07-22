using MediatR;

namespace RestaurantHR.Application.Features.Salary.Commands.DeleteSalary;

public record DeleteSalaryCommand(Guid Id) : IRequest;
