using MongoDB.Bson;

namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Interfaces;

public interface ILeadRepository
{
    Task<ICollection<LeadModel>> GetLeadsAsync();
    Task<LeadModel> PostLeadAsync(LeadModel request);
    Task<LeadModel> PutLeadAsync(ObjectId id, LeadModel request);
    Task<LeadModel> GetLeadByFilterId(LeadModel request);
    Task<bool> DeleteLeadAsync(ObjectId id);
    Task<int> GetLeadsCountByMonthAsync();
    Task<int> GetLeadsCountByWeekAsync();
    Task<int> GetMeetingsCountAsync();
    Task<(int enviados, int abertos, int respondidos)> GetEmailStatsAsync();
    Task<string?> GetTopCategoriaAsync();
    Task<TimeSpan?> GetAverageConversionTimeAsync();
    Task<int> GetLeadsCountByStatusAsync(int status);

}
