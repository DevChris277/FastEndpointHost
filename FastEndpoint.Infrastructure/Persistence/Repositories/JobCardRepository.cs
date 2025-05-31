using FastEndpoint.Application.Interfaces.Persistence;
using FastEndpoint.Domain.JobcardAggregate;
using FastEndpoint.Domain.JobCardAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FastEndpoint.Infrastructure.Persistence.Repositories;

public class JobcardRepository : IJobCardRepository
{
    private readonly FepDataContext _dbContext;

    public JobcardRepository(FepDataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(JobCard jobCard)
    {
        await _dbContext.AddAsync(jobCard);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<JobCard>> GetAllJobCards()
    {
        return await _dbContext.JobCard
            .ToListAsync();
    }

    public async Task<JobCard?> GetJobCardByJobCardId(Guid id)
    {
        var jobCardId = JobCardId.Create(id);

        return await _dbContext.JobCard
            .FirstOrDefaultAsync(j => j.Id == jobCardId);
    }

    public async Task<JobCard?> GetJobCardByJobCardName(string jobCardName)
    {
        return await _dbContext.JobCard
            .FirstOrDefaultAsync(j => j.JobCardName == jobCardName);
    }

    public async Task Update(JobCard jobCard)
    {
        _dbContext.Update(jobCard);

        await _dbContext.SaveChangesAsync();
    }
}