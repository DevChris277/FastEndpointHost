using FastEndpoint.Contracts.User.Responses;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;
using IMapper = MapsterMapper.IMapper;

namespace FastEndpoint.Api.Features.UserEndpoints.GetAllCoordinators;

public class GetAllCoordinatorsEndpoint : EndpointWithoutRequest<List<UserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetAllCoordinatorsEndpoint(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public override void Configure()
    {
        Get("/user/all/coordinators");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var users = await _userRepository.GetAllCoordinators();
        
        var response = _mapper.Map<List<UserResponse>>(users);
        await SendAsync(response, cancellation: ct);
    }
}