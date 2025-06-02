using FastEndpoint.Contracts.User.Responses;
using FastEndpoint.Domain.UserAggregate;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;

namespace FastEndpoint.Api.Features.UserEndpoints.GetAllCoordinators;

public class GetAllCoordinatorsEndpoint : EndpointWithoutRequest<List<UserResponse>,GetAllCoordinatorsMapper>
{
    private readonly IUserRepository _userRepository;

    public GetAllCoordinatorsEndpoint(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public override void Configure()
    {
        Get("/user/all/coordinators");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var users = await _userRepository.GetAllCoordinators();
        
        var response = users.Select(Map.FromEntity).ToList();
        await SendAsync(response, cancellation: ct);
    }
}