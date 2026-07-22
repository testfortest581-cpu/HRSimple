using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Shifts.Queries.GetShiftById;

public record GetShiftByIdQuery(Guid Id) : IRequest<ShiftDto?>;
