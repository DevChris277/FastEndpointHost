using FastEndpoint.Api.Features.JobcardEndpoints.UpdateJobCardStatus;
using FastEndpoint.Contracts.JobCard.Requests;
using FastEndpoint.Contracts.JobCard.Responses;
using FastEndpoint.Domain.JobcardAggregate;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;

namespace FastEndpoint.Api.Features.JobCardEndpoints.UpdateJobCardStatus;

public class UpdateJobCardStatusEndpoint : Endpoint<UpdateJobCardStatusRequest,JobCardResponse,UpdateJobCardStatusMapper>
{
    private readonly IJobCardRepository _jobCardRepository;

    public UpdateJobCardStatusEndpoint(IJobCardRepository jobCardRepository)
    {
        _jobCardRepository = jobCardRepository;
    }
    
    public override void Configure()
    {
        Put("/jobcard/update/status");
    }
    
    public override async Task HandleAsync(UpdateJobCardStatusRequest req, CancellationToken ct)
    {
        if (await _jobCardRepository.GetJobCardByJobCardId(req.JobCardId) is not JobCard jobCard)
        {
            ThrowError("JobCard not found", StatusCodes.Status404NotFound);
            return;
        }
        
        jobCard.UpdateStatus(req.Status);
        
        await _jobCardRepository.Update(jobCard);
        
        var response = Map.FromEntity(jobCard);
        await SendAsync(response, cancellation: ct);
    }
}