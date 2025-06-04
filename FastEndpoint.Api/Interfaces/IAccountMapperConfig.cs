using FastEndpoint.Contracts.Account.Responses;
using FastEndpoint.Domain.AccountAggregate;

namespace FastEndpoint.Api.Interfaces;

public interface IAccountMapperConfig
{
    public Task<List<CustomerResult>> LoadCustomersForAccount(Account account);
    public Task<AddressResult> LoadAddressForAccount(Account account);

}