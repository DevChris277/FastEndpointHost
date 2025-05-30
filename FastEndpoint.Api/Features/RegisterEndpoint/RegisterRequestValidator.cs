using FastEndpoint.Contracts.Authentication.Requests;
using FastEndpoints;
using FluentValidation;

namespace FastEndpoint.Api.Features.RegisterEndpoint;

public class RegisterRequestValidator : Validator<RegisterRequest>
{
    public RegisterRequestValidator()
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
        RuleFor(x => x.Role)
            .NotEmpty()
            .WithMessage("Role cannot be empty")
            .MaximumLength(50)
            .WithMessage("Role cannot exceed 50 characters.")
            .Must(role => role.ToLower() == "administrator" || role.ToLower() == "coordinator")
            .WithMessage("Not a valid role");
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Must be a valid Email")
            .NotEmpty()
            .WithMessage("Email cannot be empty")
            .MaximumLength(100)
            .WithMessage("Email cannot exceed 100 characters.");
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password cannot be empty")
            .MaximumLength(100)
            .WithMessage("Password cannot exceed 100 characters.");
    }
}