namespace FastEndpoint.Contracts.Authentication.Responses;

public class AuthenticationResponse
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Token { get; set; }
}
