using FastEndpoint.Contracts.JobCard.Responses;
using FastEndpoint.Domain.JobcardAggregate;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;

namespace FastEndpoint.Api.Features.JobcardEndpoints.GetJobCardById;

public class GetJobCardByIdEndpoint : EndpointWithoutRequest<JobCardResponse,GetJobCardByIdMapper>
{
    private readonly IJobCardRepository _jobCardRepository;

    public GetJobCardByIdEndpoint(IJobCardRepository jobCardRepository)
    {
        _jobCardRepository = jobCardRepository;
    }
    
    public override void Configure()
    {
        Get("/jobcard/{id}");
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        var jobCardId = Route<Guid>("id");

        if (await _jobCardRepository.GetJobCardByJobCardId(jobCardId) is not JobCard jobCard)
        {
            ThrowError("JobCard not found", StatusCodes.Status404NotFound);
            return;
        }

        var response = Map.FromEntity(jobCard);
        await SendAsync(response, cancellation: ct);
    }
}