using FastEndpoint.Api.Features.JobcardEndpoints.CreateJobCard;
using FastEndpoint.Contracts.JobCard.Requests;
using FastEndpoint.Contracts.JobCard.Responses;
using FastEndpoint.Domain.AccountAggregate.ValueObjects;
using FastEndpoint.Domain.CustomerAggregate.ValueObjects;
using FastEndpoint.Domain.JobcardAggregate;
using FastEndpoint.Domain.UserAggregate.ValueObject;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;

namespace FastEndpoint.Api.Features.JobCardEndpoints.CreateJobCard;

public class CreateJobCardEndpoint : Endpoint<CreateJobCardRequest, JobCardResponse, CreateJobCardMapper>
{
    private readonly IJobCardRepository _jobCardRepository;

    public CreateJobCardEndpoint(IJobCardRepository jobCardRepository)
    {
        _jobCardRepository = jobCardRepository;
    }
    
    public override void Configure()
    {
        Post("/jobcard/create");
    }

    public override async Task HandleAsync(CreateJobCardRequest req, CancellationToken ct)
    {
        var jobCard = JobCard.Create(
            req.JobCardName,
            req.Status,
            req.JobCardType,
            req.TeamAssigned,
            req.ClaimNumber,
            req.PolicyOption,
            req.InsuranceCompanyName,
            req.Description,
            FeUserId.Create(req.CreatedByUserId),
            FeUserId.Create(req.CoordinatorUserId),
            AccountId.Create(req.AccountId),
            CustomerId.Create(req.CustomerId));
        
        await _jobCardRepository.Add(jobCard);
        
        var response = Map.FromEntity(jobCard);
        await SendAsync(response, cancellation: ct);
    }
}