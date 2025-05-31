using FastEndpoint.Domain.UserAggregate;
using FastEndpoint.Domain.UserAggregate.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastEndpoint.Infrastructure.Persistence.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<FeUser>
{
    public void Configure(EntityTypeBuilder<FeUser> builder)
    {
        builder.ToTable("FeUsers");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => FeUserId.Create(value));

        builder.Property(u => u.FirstName)
            .HasMaxLength(100);

        builder.Property(u => u.LastName)
            .HasMaxLength(100);

        builder.Property(u => u.Role)
            .HasMaxLength(50);

        builder.Property(u => u.Password)
            .HasMaxLength(100);
    }
}