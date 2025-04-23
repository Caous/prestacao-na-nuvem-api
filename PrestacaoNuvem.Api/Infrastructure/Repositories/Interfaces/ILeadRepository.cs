using MongoDB.Bson;

namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Interfaces;

public interface ILeadRepository
{
    Task<ICollection<LeadModel>> GetLeadsAsync();
    Task<LeadModel> PostLeadAsync(LeadModel request);
    Task<LeadModel> PutLeadAsync(ObjectId id, LeadModel request);
    Task<LeadModel> GetLeadByFilterId(LeadModel request);
    Task<bool> DeleteLeadAsync(ObjectId id);
}
