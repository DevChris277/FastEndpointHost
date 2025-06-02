using FastEndpoint.Contracts.JobCard.Responses;
using FastEndpoint.Domain.JobcardAggregate;
using FastEndpoints;

namespace FastEndpoint.Api.Features.JobcardEndpoints.GetAllJobCard;

public class GetAllJobCardMapper : ResponseMapper< JobCardResponse,JobCard>
{
    public override JobCardResponse FromEntity(JobCard e) =>
    base.FromEntity(e);
}