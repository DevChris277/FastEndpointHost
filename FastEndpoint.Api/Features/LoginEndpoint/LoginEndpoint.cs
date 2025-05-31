using FastEndpoint.Application.Interfaces.Persistence;
using FastEndpoint.Application.Interfaces.Services;
using FastEndpoint.Contracts.Authentication.Requests;
using FastEndpoints;
using FastEndpoints.Contracts.Authentication.Responses;

namespace FastEndpoint.Test.Endpoints.Authentication;

public class LoginEndpoint(
    IJwtTokenProvider jwtTokenProvider,
    IUserRepository userRepository) : Endpoint<LoginRequest, AuthenticationResponse>
{
    private readonly IJwtTokenProvider _jwtTokenProvider = jwtTokenProvider;
    private readonly IUserRepository _userRepository = userRepository;

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
            ThrowError("Account not Found", "Auth.NotFound");
        
        // 2. Validate the password is correct
        if (user.Password != req.Password) ThrowError("Invalid Credentials", "Auth.InvalidCred");

        // 3. Create JWT token
        var token = _jwtTokenProvider.GenerateToken(user.FirstName, user.LastName, user.Role, user.Email);
        
        AuthenticationResponse response = new(user.FirstName, user.LastName, user.Role, user.Email, token);
        await SendAsync(response, cancellation: ct);
    }
}