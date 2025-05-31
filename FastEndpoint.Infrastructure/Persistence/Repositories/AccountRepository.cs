using FastEndpoint.Application.Interfaces.Persistence;
using FastEndpoint.Domain.AccountAggregate;
using FastEndpoint.Domain.AccountAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FastEndpoint.Infrastructure.Persistence.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly FepDataContext _dbContext;

    public AccountRepository(FepDataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Account account)
    {
        await _dbContext.AddAsync(account);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<Account?> GetAccountByAccountId(Guid id)
    {
        var accountId = AccountId.Create(id);

        return await _dbContext.Account
            .FirstOrDefaultAsync(a => a.Id == accountId);
    }

    public async Task<Account?> GetAccountByEmail(string email)
    {
        return await _dbContext.Account
            .FirstOrDefaultAsync(a => a.Email == email);
    }

    public async Task<List<Account>> GetAllAccounts()
    {
        return await _dbContext.Account.ToListAsync();
    }

    public async Task Update(Account account)
    {
        _dbContext.Update(account);

        await _dbContext.SaveChangesAsync();
    }
}