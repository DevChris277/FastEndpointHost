using FastEndpoint.Contracts.JobCard.Requests;
using FluentValidation;

namespace FastEndpoint.Api.Features.JobcardEndpoints.UpdateJobCardStatus;

public class UpdateJobCardStatusValidator : AbstractValidator<UpdateJobCardStatusRequest>
{
    public UpdateJobCardStatusValidator()
    {
        RuleFor(x => x.JobCardId)
            .NotNull()
            .NotEmpty()
            .WithMessage("Id cannot empty");
        RuleFor(x => x.Status)
            .GreaterThan(-1)
            .WithMessage("Status cannot be less than 0");
    }
}