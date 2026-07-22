using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantHR.Domain.Entities;

namespace RestaurantHR.Infrastructure.Data.Configurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.Code)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(b => b.Address)
            .HasMaxLength(500);

        builder.Property(b => b.Phone)
            .HasMaxLength(20);

        builder.HasIndex(b => b.Code).IsUnique();
    }
}
