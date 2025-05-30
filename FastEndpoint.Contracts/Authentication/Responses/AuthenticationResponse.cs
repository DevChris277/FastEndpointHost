namespace FastEndpoints.Contracts.Authentication.Responses;

public record AuthenticationResponse(
    string FirstName,
    string LastName,
    string Role,
    string Email,
    string Token)
{
}