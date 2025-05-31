using FastEndpoint.Domain.AccountAggregate.ValueObjects;
using FastEndpoint.Domain.CustomerAggregate.ValueObjects;
using FastEndpoint.Domain.JobcardAggregate;
using FastEndpoint.Domain.JobCardAggregate.ValueObjects;
using FastEndpoint.Domain.UserAggregate.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastEndpoint.Infrastructure.Persistence.Configurations;

public class JobcardConfigurations : IEntityTypeConfiguration<JobCard>
{
    public void Configure(EntityTypeBuilder<JobCard> builder)
    {
        builder.ToTable("JobCard");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => JobCardId.Create(value));

        builder.Property(c => c.JobCardName)
            .HasMaxLength(100);

        builder.Property(c => c.Status)
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(c => c.JobCardType)
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(c => c.TeamAssigned)
            .HasMaxLength(100);

        builder.Property(c => c.ClaimNumber)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.PolicyOption)
            .HasMaxLength(100);

        builder.Property(c => c.InsuranceCompanyName)
            .HasMaxLength(100);

        builder.Property(c => c.Description);

        builder.Property(c => c.ImagesVerified);

        builder.Property(c => c.CreatedByUserId)
            .HasConversion(
                id => id.Value,
                value => FeUserId.Create(value));

        builder.Property(c => c.CoordinatorUserId)
            .HasConversion(
                id => id.Value,
                value => FeUserId.Create(value));

        builder.Property(c => c.AccountId)
            .HasConversion(
                id => id.Value,
                value => AccountId.Create(value));

        builder.Property(c => c.CustomerId)
            .HasConversion(
                id => id.Value,
                value => CustomerId.Create(value));
    }
}