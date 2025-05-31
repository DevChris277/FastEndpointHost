using FastEndpoint.Domain.AccountAggregate.ValueObjects;
using FastEndpoint.Domain.AddressAggregate.ValueObject;
using FastEndpoint.Domain.CustomerAggregate;
using FastEndpoint.Domain.CustomerAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastEndpoint.Infrastructure.Persistence.Configurations;

public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => CustomerId.Create(value));

        builder.Property(c => c.FirstName)
            .HasMaxLength(100);

        builder.Property(c => c.LastName)
            .HasMaxLength(100);

        builder.Property(c => c.MobileNumber)
            .HasMaxLength(100);

        builder.Property(c => c.Email)
            .HasMaxLength(100);

        builder.Property(c => c.AddressId)
            .HasConversion(
                id => id.Value,
                value => AddressId.Create(value));

        builder.Property(c => c.AccountId)
            .HasConversion(
                id => id.Value,
                value => AccountId.Create(value));
    }
}