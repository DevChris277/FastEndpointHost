namespace FastEndpoint.Api.Features.AuthenticationEndpoints.LoginEndpoint;

public record LoginQuery(
    string Email,
    string Password);