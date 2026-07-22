using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Infrastructure.Data.Configurations;

public class EmployeeAssignmentConfiguration : IEntityTypeConfiguration<EmployeeAssignment>
{
    public void Configure(EntityTypeBuilder<EmployeeAssignment> builder)
    {
        builder.HasKey(ea => ea.Id);

        builder.HasOne(ea => ea.Employee)
            .WithMany(e => e.Assignments)
            .HasForeignKey(ea => ea.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ea => ea.Shift)
            .WithMany(s => s.EmployeeAssignments)
            .HasForeignKey(ea => ea.ShiftId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ea => ea.Branch)
            .WithMany(b => b.EmployeeAssignments)
            .HasForeignKey(ea => ea.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
