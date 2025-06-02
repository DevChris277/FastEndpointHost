using FastEndpoint.Contracts.JobCard.Requests;
using FluentValidation;

namespace FastEndpoint.Api.Features.JobcardEndpoints.UpdateJobCard;

public class UpdateJobCardValidator : AbstractValidator<UpdateJobCardRequest>
{
    public UpdateJobCardValidator()
    {
        RuleFor(x => x.JobCardName)
            .NotEmpty()
            .WithMessage("JobcardName cannot be empty")
            .MaximumLength(100)
            .WithMessage("JobcardName cannot exceed 100 characters.");
        RuleFor(x => x.Status)
            .GreaterThan(-1)
            .WithMessage("Status cannot be less than 0");
        RuleFor(x => x.JobCardType)
            .GreaterThan(-1)
            .WithMessage("JobcardType cannot be less than 0");
        RuleFor(x => x.TeamAssigned)
            .NotEmpty()
            .WithMessage("TeamAssigned cannot be empty")
            .MaximumLength(100)
            .WithMessage("TeamAssigned cannot exceed 100 characters.");
        RuleFor(x => x.ClaimNumber)
            .NotEmpty()
            .WithMessage("ClaimNumber cannot be empty")
            .MaximumLength(100)
            .WithMessage("TeamAssigned cannot exceed 100 characters.");
        RuleFor(x => x.PolicyOption)
            .NotEmpty()
            .WithMessage("PolicyOption cannot be empty")
            .MaximumLength(100)
            .WithMessage("PolicyOption cannot exceed 100 characters.");
        RuleFor(x => x.InsuranceCompanyName)
            .NotEmpty()
            .WithMessage("InsuranceCompanyName cannot be empty")
            .MaximumLength(100)
            .WithMessage("InsuranceCompanyName cannot exceed 100 characters.");
        RuleFor(x => x.CoordinatorUserId)
            .NotEmpty()
            .WithMessage("Coordinator cannot be empty")
            .WithMessage("Coordinator cannot exceed 100 characters.");
        RuleFor(x => x.AccountId)
            .NotEmpty()
            .WithMessage("AccountId cannot be empty");
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("CustomerId cannot be empty");
    }
}