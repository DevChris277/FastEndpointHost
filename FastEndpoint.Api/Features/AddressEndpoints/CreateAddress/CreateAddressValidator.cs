using FastEndpoint.Contracts.Address.Requests;
using FluentValidation;

namespace FastEndpoint.Api.Features.AddressEndpoints.CreateAddress;

public class CreateAddressValidator : AbstractValidator<CreateAddressRequest>
{
    public CreateAddressValidator()
    {
        RuleFor(x => x.Province)
            .NotEmpty()
            .WithMessage("Province cannot be empty")
            .MaximumLength(100)
            .WithMessage("Province cannot exceed 100 characters.");
        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("City cannot be empty")
            .MaximumLength(100)
            .WithMessage("City cannot exceed 100 characters.");
        RuleFor(x => x.Street)
            .NotEmpty()
            .WithMessage("Street cannot be empty")
            .MaximumLength(100)
            .WithMessage("Street cannot exceed 100 characters.");
        RuleFor(x => x.PostalCode)
            .NotEmpty()
            .WithMessage("PostalCode cannot be empty")
            .MaximumLength(100)
            .WithMessage("PostalCode cannot exceed 100 characters.");
    }
}