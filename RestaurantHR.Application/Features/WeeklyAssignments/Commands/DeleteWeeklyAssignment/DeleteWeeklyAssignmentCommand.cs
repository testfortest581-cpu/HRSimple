using MediatR;

namespace RestaurantHR.Application.Features.WeeklyAssignments.Commands.DeleteWeeklyAssignment;

public record DeleteWeeklyAssignmentCommand(Guid Id) : IRequest;
