using FastEndpoint.Domain.UserAggregate;

namespace FastEndpoint.Application.Interfaces.Services;

public interface IJwtTokenProvider
{
    string GenerateToken(FeUser user);
}