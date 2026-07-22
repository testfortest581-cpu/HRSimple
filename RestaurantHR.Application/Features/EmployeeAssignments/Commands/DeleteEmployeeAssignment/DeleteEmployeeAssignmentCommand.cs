using MediatR;

namespace RestaurantHR.Application.Features.EmployeeAssignments.Commands.DeleteEmployeeAssignment;

public record DeleteEmployeeAssignmentCommand(Guid Id) : IRequest;
