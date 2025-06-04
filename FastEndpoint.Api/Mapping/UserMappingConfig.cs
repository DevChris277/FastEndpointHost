using FastEndpoint.Contracts.User.Responses;
using FastEndpoint.Domain.UserAggregate;
using Mapster;

namespace FastEndpoint.Api.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FeUser, UserResponse>()
            .Map(dest => dest.UserId, src => Guid.Parse(src.Id.Value.ToString()));
    }
}