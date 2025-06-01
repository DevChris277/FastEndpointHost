using FastEndpoint.Api.Features.AuthenticationEndpoints.Results;
using FastEndpoint.Application.Interfaces.Persistence;
using FastEndpoint.Application.Interfaces.Services;
using FastEndpoint.Contracts.Authentication.Requests;
using FastEndpoints;
using FastEndpoints.Contracts.Authentication.Responses;
using IMapper = MapsterMapper.IMapper;

namespace FastEndpoint.Api.Features.AuthenticationEndpoints.RegisterEndpoint;

public class RegisterEndpoint : Endpoint<RegisterRequest, AuthenticationResponse>
{
    private readonly IJwtTokenProvider _jwtTokenProvider;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public RegisterEndpoint(IJwtTokenProvider jwtTokenProvider, IUserRepository userRepository, IMapper mapper)
    {
        _jwtTokenProvider = jwtTokenProvider;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public override void Configure()
    {
        Post("/auth/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        var command = _mapper.Map<RegisterCommand>(req);
        
        // 1. Validate the user exists
        if (_userRepository.GetUserByEmail(command.Email) is not null)
            ThrowError("Account Already exists", "Auth.Duplicate");
        
        // 2. Create user (generate unique ID) & Persist to DB
        var user = Domain.UserAggregate.FeUser.Create(
            command.FirstName,
            command.LastName,
            command.Role,
            command.Email,
            command.Password);
        
        await _userRepository.Add(user);

        var token = _jwtTokenProvider.GenerateToken(user);
        
        AuthenticationResult result = new(user, token);

        await SendAsync(_mapper.Map<AuthenticationResponse>(result), cancellation: ct);
    }
}