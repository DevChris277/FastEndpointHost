namespace FastEndpoint.Api.Features.AuthenticationEndpoints.RegisterEndpoint;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Role,
    string Email,
    string Password);