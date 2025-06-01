using FastEndpoint.Domain.AddressAggregate;

namespace Fastendpoint.Infrastructure.Interfaces.Persistence;

public interface IAddressRepository
{
    Task<Address?> GetAddressByProvince(string province);
    Task<Address?> GetAddressByAddressId(Guid addressId);
    Task<Address?> GetAddressByStreetNameAndPostalCode(string streetName, string postalCode);
    Task<List<Address>> GetAddressesSearch(string searchString);
    Task<List<Address>> GetAllAddresses();
    Task Add(Address address);
    Task Update(Address address);
}