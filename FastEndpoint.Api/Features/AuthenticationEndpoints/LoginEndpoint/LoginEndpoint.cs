using FastEndpoint.Contracts.Authentication.Requests;
using FastEndpoint.Contracts.Authentication.Responses;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using Fastendpoint.Infrastructure.Interfaces.Services;
using FastEndpoints;
using IMapper = MapsterMapper.IMapper;

namespace FastEndpoint.Api.Features.AuthenticationEndpoints.LoginEndpoint;

public class LoginEndpoint : Endpoint<LoginRequest, AuthenticationResponse>
{
    private readonly IJwtTokenProvider _jwtTokenProvider;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public LoginEndpoint(IUserRepository userRepository, IJwtTokenProvider jwtTokenProvider, IMapper mapper)
    {
        _userRepository = userRepository;
        _jwtTokenProvider = jwtTokenProvider;
        _mapper = mapper;
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
        
        var response = _mapper.Map<AuthenticationResponse>(user);
        response.Token = token;
        
        await SendAsync(response, cancellation: ct);
    }
}