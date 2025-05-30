namespace FastEndpoint.Contracts.Authentication.Requests;

public record LoginRequest(
    string Email,
    string Password);