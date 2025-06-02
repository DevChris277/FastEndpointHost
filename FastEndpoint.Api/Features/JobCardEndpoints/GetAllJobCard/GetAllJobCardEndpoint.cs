using FastEndpoint.Api.Features.JobcardEndpoints.GetAllJobCard;
using FastEndpoint.Contracts.JobCard.Responses;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;

namespace FastEndpoint.Api.Features.JobCardEndpoints.GetAllJobCard;

public class GetAllJobCardEndpoint : EndpointWithoutRequest<List<JobCardResponse>,GetAllJobCardMapper>
{
    private readonly IJobCardRepository _jobCardRepository;

    public GetAllJobCardEndpoint(IJobCardRepository jobCardRepository)
    {
        _jobCardRepository = jobCardRepository;
    }
    
    public override void Configure()
    {
        Get("/jobcard/all");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var jobCards = await _jobCardRepository.GetAllJobCards();
        
        var response = jobCards.Select(Map.FromEntity).ToList();
        await SendAsync(response, cancellation: ct);
    }
}