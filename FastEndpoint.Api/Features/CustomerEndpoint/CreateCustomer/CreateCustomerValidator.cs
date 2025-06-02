using FastEndpoint.Contracts.Customer.Requests;
using FluentValidation;

namespace FastEndpoint.Api.Features.CustomerEndpoint.CreateCustomer;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("FirstName cannot be empty")
            .MaximumLength(100)
            .WithMessage("FirstName cannot exceed 100 characters.");
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("LastName cannot be empty")
            .MaximumLength(100)
            .WithMessage("LastName cannot exceed 100 characters.");
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
        RuleFor(x => x.AccountId)
            .NotEmpty()
            .WithMessage("AccountId cannot be empty");
        RuleFor(x => x.AddressId)
            .NotEmpty()
            .WithMessage("AddressId cannot be empty");
    }
}