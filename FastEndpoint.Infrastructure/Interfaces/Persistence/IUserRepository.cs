using FastEndpoint.Domain.UserAggregate;
using FastEndpoint.Domain.UserAggregate.ValueObject;

namespace Fastendpoint.Infrastructure.Interfaces.Persistence;

public interface IUserRepository
{
    FeUser? GetUserByEmail(string email);
    Task<FeUser?> GetUserById(Guid id);
    Task<FeUser?> GetUserById(FeUserId id);
    Task Add(FeUser user);
    Task<List<FeUser>> GetAllCoordinators(string role);
}