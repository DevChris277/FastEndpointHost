using FastEndpoint.Contracts.JobCard.Requests;
using FastEndpoint.Contracts.JobCard.Responses;
using FastEndpoint.Domain.AccountAggregate.ValueObjects;
using FastEndpoint.Domain.CustomerAggregate.ValueObjects;
using FastEndpoint.Domain.JobcardAggregate;
using FastEndpoint.Domain.UserAggregate.ValueObject;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;

namespace FastEndpoint.Api.Features.JobcardEndpoints.UpdateJobCard;

public class UpdateJobCardEndpoint : Endpoint<UpdateJobCardRequest,JobCardResponse,UpdateJobCardMapper>
{
    private readonly IJobCardRepository _jobCardRepository;

    public UpdateJobCardEndpoint(IJobCardRepository jobCardRepository)
    {
        _jobCardRepository = jobCardRepository;
    }

    public override void Configure()
    {
        Put("/jobcard/update");
    }

    public override async Task HandleAsync(UpdateJobCardRequest req, CancellationToken ct)
    {
        if (await _jobCardRepository.GetJobCardByJobCardId(req.JobCardId) is not JobCard jobCard)
        {
            ThrowError("JobCard not found", StatusCodes.Status404NotFound);
            return;
        }
        
        jobCard.Update(
            req.JobCardName,
            req.Status,
            req.JobCardType,
            req.TeamAssigned,
            req.ClaimNumber,
            req.PolicyOption,
            req.InsuranceCompanyName,
            req.Description,
            req.ImagesVerified,
            FeUserId.Create(jobCard.CreatedByUserId.Value),
            FeUserId.Create(req.CoordinatorUserId),
            AccountId.Create(req.AccountId),
            CustomerId.Create(req.CustomerId));

        await _jobCardRepository.Update(jobCard);
        
        var response = Map.FromEntity(jobCard);
        await SendAsync(response, cancellation: ct);
    }
}