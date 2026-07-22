using RestaurantHR.Domain.Common;

namespace RestaurantHR.Domain.Entities;

public class Branch : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    public ICollection<EmployeeAssignment> EmployeeAssignments { get; set; } = new List<EmployeeAssignment>();
}
