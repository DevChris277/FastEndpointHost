using FastEndpoint.Contracts.Customer.Responses;
using FastEndpoint.Domain.AccountAggregate;
using FastEndpoint.Domain.CustomerAggregate.ValueObjects;
using Fastendpoint.Infrastructure.Interfaces.Persistence;
using FastEndpoints;

namespace FastEndpoint.Api.Features.CustomerEndpoint.CreateCustomer;

public class CreateCustomerEvent<TRequest, TResponse> : IPostProcessor<TRequest, TResponse>
{
    public async Task PostProcessAsync(IPostProcessorContext<TRequest, TResponse> context, CancellationToken ct)
    {
        if (context.Response is CustomerResponse response)
        {
            var _accountRepository = context.HttpContext.RequestServices
                .GetRequiredService<IAccountRepository>();

            if (await _accountRepository.GetAccountByAccountId(response.AccountId) is not Account account)
            {
                throw new InvalidOperationException($"Customer has invalid account id (customer id: {response.CustomerId}," +
                                                    $" account id: {response.AccountId}).");
            }

            account.AddCustomerId(CustomerId.Create(response.CustomerId));
            
            await _accountRepository.Update(account);
        }
            
        
    }
}