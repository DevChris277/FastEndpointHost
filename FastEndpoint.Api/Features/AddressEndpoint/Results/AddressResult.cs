using FastEndpoint.Domain.AddressAggregate;

namespace FastEndpoint.Api.Features.AddressEndpoint.Results;

public record AddressResult(
    Address Address,
    Guid AddressId);