using FastEndpoint.Application.Interfaces.Persistence;
using FastEndpoint.Application.Interfaces.Services;
using FastEndpoint.Contracts.Authentication.Requests;
using FastEndpoints;
using FastEndpoints.Contracts.Authentication.Responses;

namespace FastEndpoint.Api.Features.AuthenticationEndpoints.LoginEndpoint;

public class LoginEndpoint : Endpoint<LoginRequest, AuthenticationResponse, LoginMapper>
{
    private readonly IJwtTokenProvider _jwtTokenProvider;
    private readonly IUserRepository _userRepository;

    public LoginEndpoint(IUserRepository userRepository, IJwtTokenProvider jwtTokenProvider)
    {
        _userRepository = userRepository;
        _jwtTokenProvider = jwtTokenProvider;
    }

    public override void Configure()
    {
        Post("/auth/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        // 1. Validate the user exists
        var user = _userRepository.GetUserByEmail(req.Email);
        if (user is null)
            ThrowError("Account not Found", StatusCodes.Status404NotFound);
        
        // 2. Validate the password is correct
        if (user.Password != req.Password) ThrowError("Invalid Credentials", StatusCodes.Status401Unauthorized);

        // 3. Create JWT token
        var token = _jwtTokenProvider.GenerateToken(user);
        
        AuthenticationResponse response = Map.FromEntityWithToken(user, token);
        
        await SendAsync(response, cancellation: ct);
    }
}