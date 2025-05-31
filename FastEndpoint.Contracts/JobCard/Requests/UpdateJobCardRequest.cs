namespace FastEndpoint.Contracts.JobCard.Requests;

public record UpdateJobCardRequest(
    Guid JobCardId,
    string JobCardName,
    int Status,
    int JobCardType,
    string TeamAssigned,
    string ClaimNumber,
    string PolicyOption,
    string InsuranceCompanyName,
    string Description,
    bool ImagesVerified,
    Guid CoordinatorUserId,
    Guid AccountId,
    Guid CustomerId);