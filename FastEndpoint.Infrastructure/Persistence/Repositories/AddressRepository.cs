using FastEndpoint.Application.Interfaces.Persistence;
using FastEndpoint.Domain.AddressAggregate;
using FastEndpoint.Domain.AddressAggregate.ValueObject;
using Microsoft.EntityFrameworkCore;

namespace FastEndpoint.Infrastructure.Persistence.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly FepDataContext _dbContext;

    public AddressRepository(FepDataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Address address)
    {
        await _dbContext.AddAsync(address);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<Address?> GetAddressByProvince(string province)
    {
        return await _dbContext.Address
            .FirstOrDefaultAsync(a => a.Province == province);
    }

    public async Task<Address?> GetAddressByAddressId(Guid id)
    {
        var addressId = AddressId.Create(id);

        return await _dbContext.Address
            .FirstOrDefaultAsync(a => a.Id == addressId);
    }

    public async Task Update(Address address)
    {
        _dbContext.Update(address);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Address>> GetAllAddresses()
    {
        return await _dbContext.Address.ToListAsync();
    }

    public async Task<Address?> GetAddressByStreetNameAndPostalCode(string streetName, string postalCode)
    {
        return await _dbContext.Address
            .FirstOrDefaultAsync(a => a.Street == streetName && a.PostalCode == postalCode);
    }

    public async Task<List<Address>> GetAddressesSearch(string searchString)
    {
        return await _dbContext.Address
            .Where(a => EF.Functions.Like(a.Province, $"%{searchString}%")
                        || EF.Functions.Like(a.Street, $"%{searchString}%")
                        || EF.Functions.Like(a.PostalCode, $"%{searchString}%")
                        || EF.Functions.Like(a.City, $"%{searchString}%"))
            .ToListAsync();
    }
}