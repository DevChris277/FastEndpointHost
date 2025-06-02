using FastEndpoint.Domain.UserAggregate;
using FastEndpoint.Domain.UserAggregate.ValueObject;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FastEndpoint.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly FepDataContext _dbContext;

    public UserRepository(FepDataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(FeUser user)
    {
        await _dbContext.AddAsync(user);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<FeUser>> GetAllCoordinators()
    {
        //ToDo: Add role check
        return await _dbContext.FeUser
            .Where(u => u.Role == "coordinator").ToListAsync();
    }

    public FeUser? GetUserByEmail(string email)
    {
        return _dbContext.FeUser
            .SingleOrDefault(u => u.Email == email);
    }

    public async Task<FeUser?> GetUserById(Guid id)
    {
        var userId = FeUserId.Create(id);

        return await _dbContext.FeUser
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<FeUser?> GetUserById(FeUserId id)
    {
        return await _dbContext.FeUser
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}