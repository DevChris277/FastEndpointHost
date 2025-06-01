using FastEndpoint.Domain.Common.Settings;
using FastEndpoint.Domain.UserAggregate;
using Fastendpoint.Infrastructure.Interfaces.Services;
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

    public string GenerateToken(FeUser user)
    {
        var jwt = JwtBearer.CreateToken(options =>
        {
            options.SigningKey = _jwtSettings.Secret;
            options.ExpireAt = DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes);
            options.Issuer = _jwtSettings.Issuer;
            options.Audience = _jwtSettings.Audience;
            options.User.Roles.Add(user.Role);
            options.User.Claims.Add(("Email", user.Email));
            options.User.Claims.Add(("UserName", $"{user.FirstName} {user.LastName}"));
        });

        return jwt;
    }
}
