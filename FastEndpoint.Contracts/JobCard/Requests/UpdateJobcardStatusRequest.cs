namespace FastEndpoint.Contracts.JobCard.Requests;

public record UpdateJobCardStatusRequest(
    Guid JobCardId,
    int Status);