using FastEndpoint.Domain.UserAggregate;

namespace Fastendpoint.Infrastructure.Interfaces.Services;

public interface IJwtTokenProvider
{
    string GenerateToken(FeUser user);
}