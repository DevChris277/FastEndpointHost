using FastEndpoint.Domain.UserAggregate;

namespace FastEndpoint.Api.Features.AuthenticationEndpoints.Results;

public record AuthenticationResult(
    FeUser User,
    string Token);