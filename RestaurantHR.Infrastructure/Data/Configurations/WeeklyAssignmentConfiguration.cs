using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Infrastructure.Data.Configurations;

public class WeeklyAssignmentConfiguration : IEntityTypeConfiguration<WeeklyAssignment>
{
    public void Configure(EntityTypeBuilder<WeeklyAssignment> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.Employee)
            .WithMany()
            .HasForeignKey(e => e.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Branch)
            .WithMany()
            .HasForeignKey(e => e.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Shift)
            .WithMany()
            .HasForeignKey(e => e.ShiftId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
