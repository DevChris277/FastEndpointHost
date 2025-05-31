using FastEndpoint.Domain.JobcardAggregate;

namespace FastEndpoint.Application.Interfaces.Persistence;

public interface IJobCardRepository
{
    Task<JobCard?> GetJobCardByJobCardName(string jobCardName);
    Task<JobCard?> GetJobCardByJobCardId(Guid jobCardId);
    Task<List<JobCard>> GetAllJobCards();
    Task Add(JobCard jobCard);
    Task Update(JobCard jobCard);
}