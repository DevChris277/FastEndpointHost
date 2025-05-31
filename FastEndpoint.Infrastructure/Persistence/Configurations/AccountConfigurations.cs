using FastEndpoint.Domain.AccountAggregate;
using FastEndpoint.Domain.AccountAggregate.ValueObjects;
using FastEndpoint.Domain.AddressAggregate.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastEndpoint.Infrastructure.Persistence.Configurations;

public class AccountConfigurations : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        ConfigureAccountTable(builder);
        ConfigureAccountCustomerIdsTable(builder);
    }

    private static void ConfigureAccountTable(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Account");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => AccountId.Create(value));

        builder.Property(a => a.Name)
            .HasMaxLength(100);

        builder.Property(a => a.MobileNumber)
            .HasMaxLength(100);

        builder.Property(a => a.Email)
            .HasMaxLength(100);

        builder.Property(a => a.AddressId)
            .HasConversion(
                id => id.Value,
                value => AddressId.Create(value));
    }

    private static void ConfigureAccountCustomerIdsTable(EntityTypeBuilder<Account> builder)
    {
        builder.OwnsMany(m => m.CustomerIds, dib =>
        {
            dib.ToTable("AccountCustomerIds");

            dib.WithOwner().HasForeignKey("AccountId");

            dib.HasKey("Id");

            dib.Property(d => d.Value)
                .HasColumnName("CustomerId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Account.CustomerIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}