namespace FastEndpoint.Application.Interfaces.Services;

public interface IJwtTokenProvider
{
    string GenerateToken(string firstName, string lastName, string role, string email);
}