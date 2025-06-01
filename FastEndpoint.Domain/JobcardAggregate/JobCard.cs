using FastEndpoint.Domain.AccountAggregate.ValueObjects;
using FastEndpoint.Domain.Common.Models;
using FastEndpoint.Domain.CustomerAggregate.ValueObjects;
using FastEndpoint.Domain.JobCardAggregate.ValueObjects;
using FastEndpoint.Domain.UserAggregate.ValueObject;

namespace FastEndpoint.Domain.JobcardAggregate;

public sealed class JobCard : AggregateRoot<JobCardId, Guid>
{
    public string JobCardName { get; private set; } = null!;
    public int Status { get; private set; }
    public int JobCardType { get; private set; }
    public string TeamAssigned { get; private set; } = null!;
    public string ClaimNumber { get; private set; }
    public string PolicyOption { get; private set; } = null!;
    public string InsuranceCompanyName { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public bool ImagesVerified { get; private set; } = false;
    public FeUserId CreatedByUserId { get; private set; } = null!;
    public FeUserId CoordinatorUserId { get; private set; } = null!;
    public AccountId AccountId { get; private set; } = null!;
    public CustomerId CustomerId { get; private set; } = null!;

    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private JobCard(
       JobCardId jobCardId,
       string jobCardName,
       int status,
       int jobCardType,
       string teamAssigned,
       string claimNumber,
       string policyOption,
       string insuranceCompanyName,
       string description,
       bool imagesVerified,
       FeUserId createdByUserId,
       FeUserId coordinatorUserId,
       AccountId accountId,
       CustomerId customerId)
       : base(jobCardId)
    {
        JobCardName = jobCardName;
        Status = status;
        JobCardType = jobCardType;
        TeamAssigned = teamAssigned;
        ClaimNumber = claimNumber;
        PolicyOption = policyOption;
        InsuranceCompanyName = insuranceCompanyName;
        Description = description;
        ImagesVerified = imagesVerified;
        CreatedByUserId = createdByUserId;
        CoordinatorUserId = coordinatorUserId;
        AccountId = accountId;
        CustomerId = customerId;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public static JobCard Create(
        string jobCardName,
        int status,
        int jobCardType,
        string teamAssigned,
        string claimNumber,
        string policyOption,
        string insuranceCompanyName,
        string description,
        FeUserId createdByUserId,
        FeUserId coordinatorUserId,
        AccountId accountId,
        CustomerId customerId)
    {
        // TODO: enforce invariants
        var jobCard = new JobCard(
            JobCardId.CreateUnique(),
            jobCardName,
            status,
            jobCardType,
            teamAssigned,
            claimNumber,
            policyOption,
            insuranceCompanyName,
            description,
            false,
            createdByUserId,
            coordinatorUserId,
            accountId,
            customerId);

        // jobCard.AddDomainEvent(new JobCardCreated(jobCard));

        return jobCard;
    }

    public void Update(
        string jobCardName,
        int status,
        int jobCardType,
        string teamAssigned,
        string claimNumber,
        string policyOption,
        string insuranceCompanyName,
        string description,
        bool imagesVerified,
        FeUserId createdByUserId,
        FeUserId coordinatorUserId,
        AccountId accountId,
        CustomerId customerId)
    {
        JobCardName = jobCardName;
        Status = status;
        JobCardType = jobCardType;
        TeamAssigned = teamAssigned;
        ClaimNumber = claimNumber;
        PolicyOption = policyOption;
        InsuranceCompanyName = insuranceCompanyName;
        Description = description;
        ImagesVerified = imagesVerified;
        CreatedByUserId = createdByUserId;
        CoordinatorUserId = coordinatorUserId;
        AccountId = accountId;
        CustomerId = customerId;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public void UpdateStatus(int status)
    {
        Status = status;
        UpdatedDateTime = DateTime.UtcNow;
    }

#pragma warning disable CS8618
    private JobCard()
    {
    }
#pragma warning restore CS8618
}