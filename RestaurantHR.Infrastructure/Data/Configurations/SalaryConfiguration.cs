using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Infrastructure.Data.Configurations;

public class SalaryConfiguration : IEntityTypeConfiguration<Salary>
{
    public void Configure(EntityTypeBuilder<Salary> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.BaseSalary)
            .HasColumnType("decimal(18,2)");

        builder.Property(s => s.Overtime)
            .HasColumnType("decimal(18,2)");

        builder.Property(s => s.Bonus)
            .HasColumnType("decimal(18,2)");

        builder.Property(s => s.Deductions)
            .HasColumnType("decimal(18,2)");

        builder.Property(s => s.Notes)
            .HasMaxLength(500);

        builder.HasOne(s => s.Employee)
            .WithMany(e => e.Salaries)
            .HasForeignKey(s => s.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
