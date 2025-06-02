using FastEndpoint.Contracts.JobCard.Responses;
using FastEndpoint.Domain.JobcardAggregate;
using FastEndpoints;

namespace FastEndpoint.Api.Features.JobcardEndpoints.GetAllJobCard;

public class GetAllJobCardMapper : ResponseMapper< JobCardResponse,JobCard>
{
    public override JobCardResponse FromEntity(JobCard e) =>
        new JobCardResponse(
            e.Id.Value,
            e.JobCardName,
            e.Status,
            e.JobCardType,
            e.TeamAssigned,
            e.ClaimNumber,
            e.PolicyOption,
            e.InsuranceCompanyName,
            e.Description,
            e.ImagesVerified,
            e.CoordinatorUserId.Value,
            e.AccountId.Value,
            e.CustomerId.Value,
            e.CreatedDateTime,
            e.UpdatedDateTime
        );

}