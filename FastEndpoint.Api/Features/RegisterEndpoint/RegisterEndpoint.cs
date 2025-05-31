using FastEndpoint.Application.Interfaces.Persistence;
using FastEndpoint.Application.Interfaces.Services;
using FastEndpoint.Contracts.Authentication.Requests;
using FastEndpoints;
using FastEndpoints.Contracts.Authentication.Responses;

namespace FastEndpoint.Api.Features.RegisterEndpoint;

public class RegisterEndpoint(IJwtTokenProvider jwtTokenProvider, IUserRepository userRepository)
    : Endpoint<RegisterRequest, AuthenticationResponse>
{
    private readonly IJwtTokenProvider _jwtTokenProvider = jwtTokenProvider;
    private readonly IUserRepository _userRepository = userRepository;

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
        var user = Domain.UserAggregate.FeUser.Create(
            req.FirstName,
            req.LastName,
            req.Role,
            req.Email,
            req.Password);
        
        await _userRepository.Add(user);

        var token = _jwtTokenProvider.GenerateToken(user.FirstName, user.LastName, user.Role, user.Email);

        AuthenticationResponse response = new(user.FirstName, user.LastName, user.Role, user.Email, token);

        await SendAsync(response, cancellation: ct);
    }
}