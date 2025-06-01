using FastEndpoint.Api.Features.AddressEndpoint.CreateAddress;
using FastEndpoint.Api.Features.AuthenticationEndpoints.Results;
using FastEndpoint.Application.Interfaces.Persistence;
using FastEndpoint.Application.Interfaces.Services;
using FastEndpoint.Contracts.Authentication.Requests;
using FastEndpoints;
using FastEndpoints.Contracts.Authentication.Responses;
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
        var query = _mapper.Map<LoginQuery>(req);
        
        // 1. Validate the user exists
        var user = _userRepository.GetUserByEmail(query.Email);
        if (user is null)
            ThrowError("Account not Found", "Auth.NotFound");
        
        // 2. Validate the password is correct
        if (user.Password != query.Password) ThrowError("Invalid Credentials", "Auth.InvalidCred");

        // 3. Create JWT token
        var token = _jwtTokenProvider.GenerateToken(user);
        
        AuthenticationResult result = new(user, token);
        
        await SendAsync(_mapper.Map<AuthenticationResponse>(result), cancellation: ct);
    }
}