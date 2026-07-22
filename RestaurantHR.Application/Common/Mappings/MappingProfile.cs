using AutoMapper;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Branch, DTOs.BranchDto>();
        CreateMap<Employee, DTOs.EmployeeDto>();
        CreateMap<Shift, DTOs.ShiftDto>();
        CreateMap<Leave, DTOs.LeaveDto>();
        CreateMap<Attendance, DTOs.AttendanceDto>();
        CreateMap<Salary, DTOs.SalaryDto>();
        CreateMap<Payment, DTOs.PaymentDto>();
        CreateMap<EmployeeAssignment, DTOs.EmployeeAssignmentDto>();
    }
}
