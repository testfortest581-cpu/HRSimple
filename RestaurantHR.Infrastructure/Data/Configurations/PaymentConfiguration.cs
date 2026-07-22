using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Infrastructure.Data.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Amount)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.Property(p => p.PaymentType)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasOne(p => p.Employee)
            .WithMany(e => e.Payments)
            .HasForeignKey(p => p.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Salary)
            .WithMany(s => s.Payments)
            .HasForeignKey(p => p.SalaryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
