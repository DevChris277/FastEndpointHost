using FastEndpoint.Contracts.User.Responses;
using FastEndpoint.Domain.UserAggregate;
using FastEndpoints;

namespace FastEndpoint.Api.Features.UserEndpoints.GetAllCoordinators;

public class GetAllCoordinatorsMapper : ResponseMapper<UserResponse,FeUser>
{
    public override UserResponse FromEntity(FeUser e) =>
        new UserResponse(
            e.Id.Value,
            e.FirstName,
            e.LastName,
            e.Role,
            e.Email
        );

}