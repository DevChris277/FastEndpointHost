using FastEndpoint.Domain.AccountAggregate;

namespace FastEndpoint.Application.Interfaces.Persistence;

public interface IAccountRepository
{
    Task<Account?> GetAccountByEmail(string email);
    Task<Account?> GetAccountByAccountId(Guid id);
    Task<List<Account>> GetAllAccounts();
    Task Add(Account account);
    Task Update(Account account);
}