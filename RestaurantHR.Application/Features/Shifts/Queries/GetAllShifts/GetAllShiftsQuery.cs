using MediatR;
using RestaurantHR.Application.DTOs;

namespace RestaurantHR.Application.Features.Shifts.Queries.GetAllShifts;

public record GetAllShiftsQuery : IRequest<List<ShiftDto>>;
