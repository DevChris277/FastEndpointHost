using FastEndpoint.Application.Interfaces.Services;
using FastEndpoint.Domain.Common.Settings;
using FastEndpoints.Security;
using Microsoft.Extensions.Options;

namespace FastEndpoint.Infrastructure.Services;

public class JwtTokenProvider : IJwtTokenProvider
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenProvider(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateToken(string firstName, string lastName, string role, string email)
    {
        var jwt = JwtBearer.CreateToken(options =>
        {
            options.SigningKey = _jwtSettings.Secret;
            options.ExpireAt = DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes);
            options.Issuer = _jwtSettings.Issuer;
            options.Audience = _jwtSettings.Audience;
            options.User.Roles.Add(role);
            options.User.Claims.Add(("Email", email));
            options.User.Claims.Add(("UserName", $"{firstName} {lastName}"));
        });

        return jwt;
    }
}
