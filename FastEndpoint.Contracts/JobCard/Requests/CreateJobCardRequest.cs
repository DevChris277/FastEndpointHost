namespace FastEndpoint.Contracts.JobCard.Requests;

public record CreateJobCardRequest(
    string JobCardName,
    int Status,
    int JobCardType,
    string TeamAssigned,
    string ClaimNumber,
    string PolicyOption,
    string InsuranceCompanyName,
    string Description,
    Guid CreatedByUserId,
    Guid CoordinatorUserId,
    Guid AccountId,
    Guid CustomerId);