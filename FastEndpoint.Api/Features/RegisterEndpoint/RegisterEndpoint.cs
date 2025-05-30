using FastEndpoint.Application.Interfaces.Services;
using FastEndpoint.Contracts.Authentication.Requests;
using FastEndpoints;
using FastEndpoints.Contracts.Authentication.Responses;

namespace FastEndpoint.Api.Features.RegisterEndpoint;

public class RegisterEndpoint(IJwtTokenProvider jwtTokenProvider)
    : Endpoint<RegisterRequest, AuthenticationResponse>
{
    private readonly IJwtTokenProvider _jwtTokenProvider = jwtTokenProvider;

    public override void Configure()
    {
        Post("/api/auth/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        // // 1. Validate the user exists
        // if (_userRepository.GetUserByEmail(req.Email) is not null)
        //     ThrowError("Account Already exists", "Auth.Duplicate");
        //
        // // 2. Create user (generate unique ID) & Persist to DB
        // var user = Domain.UserAggregate.User.Create(
        //     req.FirstName,
        //     req.LastName,
        //     req.Role,
        //     req.Email,
        //     req.Password);
        //
        // await _userRepository.Add(user);

        var token = _jwtTokenProvider.GenerateToken(req.FirstName, req.LastName, req.Role, req.Email);

        AuthenticationResponse response = new(req.FirstName, req.LastName, req.Role, req.Email, token);

        await SendAsync(response, cancellation: ct);
    }
}