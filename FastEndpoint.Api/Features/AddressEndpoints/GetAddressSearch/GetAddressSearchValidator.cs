using FastEndpoint.Contracts.Address.Requests;
using FluentValidation;

namespace FastEndpoint.Api.Features.AddressEndpoints.GetAddressSearch;

public class GetAddressesSearchQueryValidator : AbstractValidator<GetAddressesSearchRequest>
{
    public GetAddressesSearchQueryValidator()
    {
        RuleFor(x => x.SearchString)
            .NotEmpty()
            .WithMessage("Search cannot be empty");
    }
}