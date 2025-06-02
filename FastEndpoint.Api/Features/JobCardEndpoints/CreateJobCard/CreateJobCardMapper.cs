using FastEndpoint.Contracts.JobCard.Requests;
using FastEndpoint.Contracts.JobCard.Responses;
using FastEndpoint.Domain.JobcardAggregate;
using FastEndpoints;

namespace FastEndpoint.Api.Features.JobcardEndpoints.CreateJobCard;

public class CreateJobCardMapper : Mapper<CreateJobCardRequest,JobCardResponse, JobCard>
{
    public override JobCardResponse FromEntity(JobCard e) =>
    base.FromEntity(e);
}