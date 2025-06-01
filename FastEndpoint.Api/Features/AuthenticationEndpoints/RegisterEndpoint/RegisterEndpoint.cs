using FastEndpoint.Contracts.Authentication.Requests;
using FastEndpoint.Domain.UserAggregate;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using Fastendpoint.Infrastructure.Interfaces.Services;
using FastEndpoints;
using FastEndpoints.Contracts.Authentication.Responses;

namespace FastEndpoint.Api.Features.AuthenticationEndpoints.RegisterEndpoint;

public class RegisterEndpoint : Endpoint<RegisterRequest, AuthenticationResponse, RegisterMapper>
{
    private readonly IJwtTokenProvider _jwtTokenProvider;
    private readonly IUserRepository _userRepository;
    public RegisterEndpoint(IJwtTokenProvider jwtTokenProvider, IUserRepository userRepository)
    {
        _jwtTokenProvider = jwtTokenProvider;
        _userRepository = userRepository;
    }
    
    public override void Configure()
    {
        Post("/auth/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        
        // 1. Validate the user exists
        if (_userRepository.GetUserByEmail(req.Email) is not null)
            ThrowError("Account Already exists", "Auth.Duplicate");
        
        // 2. Create user (generate unique ID) & Persist to DB
        var user = FeUser.Create(
            req.FirstName,
            req.LastName,
            req.Role,
            req.Email,
            req.Password);
        
        await _userRepository.Add(user);

        var token = _jwtTokenProvider.GenerateToken(user);
        
        AuthenticationResponse response = Map.FromEntityWithToken(user, token);

        await SendAsync(response, cancellation: ct);
    }
}