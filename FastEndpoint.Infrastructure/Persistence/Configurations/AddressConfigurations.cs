using FastEndpoint.Domain.AddressAggregate;
using FastEndpoint.Domain.AddressAggregate.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastEndpoint.Infrastructure.Persistence.Configurations;

public class AddressConfigurations : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Address");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => AddressId.Create(value));

        builder.Property(a => a.City)
            .HasMaxLength(100);

        builder.Property(a => a.Province)
            .HasMaxLength(100);

        builder.Property(a => a.Street)
            .HasMaxLength(100);

        builder.Property(a => a.PostalCode)
            .HasMaxLength(100);
    }
}