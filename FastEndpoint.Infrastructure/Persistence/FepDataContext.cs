using FastEndpoint.Domain.AccountAggregate;
using FastEndpoint.Domain.AddressAggregate;
using FastEndpoint.Domain.CustomerAggregate;
using FastEndpoint.Domain.JobcardAggregate;
using FastEndpoint.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;


namespace FastEndpoint.Infrastructure.Persistence;

public class FepDataContext : DbContext
{
    public FepDataContext(DbContextOptions<FepDataContext> options)
        : base(options)
    {
        
    }

    public DbSet<FeUser> FeUser { get; set; } = null!;
    public DbSet<Account> Account { get; set; } = null!;
    public DbSet<Customer> Customer { get; set; } = null!;
    public DbSet<Address> Address { get; set; } = null!;
    public DbSet<JobCard> JobCard { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(FepDataContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}