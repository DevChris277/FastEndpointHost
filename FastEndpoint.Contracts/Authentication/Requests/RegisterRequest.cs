namespace FastEndpoint.Contracts.Authentication.Requests;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string Role,
    string Email,
    string Password);