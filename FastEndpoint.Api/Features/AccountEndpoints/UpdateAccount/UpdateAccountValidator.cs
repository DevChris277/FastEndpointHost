using FastEndpoint.Contracts.Account.Requests;
using FluentValidation;

namespace FastEndpoint.Api.Features.AccountEndpoints.UpdateAccount;

public class UpdateAccountValidator: AbstractValidator<UpdateAccountRequest>
{
    public UpdateAccountValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty")
            .MaximumLength(100)
            .WithMessage("Name cannot exceed 100 characters.");
        RuleFor(x => x.MobileNumber)
            .NotEmpty()
            .WithMessage("MobileNumber cannot be empty")
            .MaximumLength(100)
            .WithMessage("MobileNumber cannot exceed 100 characters.");
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty")
            .MaximumLength(100)
            .WithMessage("Email cannot exceed 100 characters.");
        RuleFor(x => x.AddressId)
            .NotEmpty()
            .WithMessage("AddressId cannot be empty");
    }
}