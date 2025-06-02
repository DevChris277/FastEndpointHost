using FastEndpoint.Contracts.Authentication.Requests;
using FastEndpoint.Domain.UserAggregate;
using FastEndpoints;
using FastEndpoints.Contracts.Authentication.Responses;

namespace FastEndpoint.Api.Features.AuthenticationEndpoints.LoginEndpoint;

public class LoginMapper : Mapper<LoginRequest,AuthenticationResponse,FeUser>
{
    public override FeUser ToEntity(LoginRequest r) 
    {
        FeUser entity = base.ToEntity(r);
        return entity;
    }

    public AuthenticationResponse FromEntityWithToken(FeUser e,string token) => new(e.FirstName,e.LastName,e.Role,e.Email,token);
}