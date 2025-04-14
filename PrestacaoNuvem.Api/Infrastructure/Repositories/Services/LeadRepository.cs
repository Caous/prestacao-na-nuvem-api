using MongoDB.Bson;
using MongoDB.Driver;

namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Services;

public class LeadRepository : ILeadRepository
{
    private const string CollectionsGroupMessages = "leadsinnova";
    private readonly IMongoDatabase _dataBase;
    public LeadRepository(IMongoDatabase database)
    {
        _dataBase = database;
    }

    public async Task<bool> DeleteLeadAsync(ObjectId id)
    {
        var collection = _dataBase.GetCollection<LeadModel>(CollectionsGroupMessages);

        var filter = Builders<LeadModel>.Filter.Eq("_id", id);

        var deleteResult = await collection.DeleteOneAsync(filter);

        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }

    public async Task<LeadModel> GetLeadByFilterId(LeadModel request)
    {
        var collection = _dataBase.GetCollection<LeadModel>(CollectionsGroupMessages);

        var filters = new List<FilterDefinition<LeadModel>>();

        if (!string.IsNullOrEmpty(request.Name))
            filters.Add(Builders<LeadModel>.Filter.Eq("Name", request.Name));

        if (!string.IsNullOrEmpty(request.PhoneNumber))
            filters.Add(Builders<LeadModel>.Filter.Eq("PhoneNumber", request.PhoneNumber));

        if (!string.IsNullOrEmpty(request.Category))
            filters.Add(Builders<LeadModel>.Filter.Eq("Category", request.Category));

        var combinedFilter = filters.Count > 0
            ? Builders<LeadModel>.Filter.And(filters)
            : Builders<LeadModel>.Filter.Empty;

        var lead = await collection
            .Find(combinedFilter)
            .FirstOrDefaultAsync();

        return lead;
    }

    public async Task<ICollection<LeadModel>> GetLeadsAsync()
    {
        var collection = _dataBase.GetCollection<LeadModel>(CollectionsGroupMessages);

        var group = await collection
            .Find(Builders<LeadModel>.Filter.Empty)
            .ToListAsync();

        return group;
    }

    public async Task<LeadModel> PostLeadAsync(LeadModel request)
    {
        var collection = _dataBase.GetCollection<LeadModel>(CollectionsGroupMessages);

        if (request != null)
            collection.InsertOneAsync(request).GetAwaiter().GetResult();

        return request;
    }

    public async Task<LeadModel> PutLeadAsync(ObjectId id, LeadModel request)
    {
        var collection = _dataBase.GetCollection<LeadModel>(CollectionsGroupMessages);

        if (request != null)
        {
            var filter = Builders<LeadModel>.Filter.Eq("_id", id);

            var xpto = await collection.FindAsync(filter);

            var updateDefinition = Builders<LeadModel>.Update
                                       .Set(x => x.Name, request.Name)
                                       .Set(x => x.Category, request.Category)
                                       .Set(x => x.Address, request.Address)
                                       .Set(x => x.TimeOpen, request.TimeOpen)
                                       .Set(x => x.Star, request.Star)
                                       .Set(x => x.WebSite, request.WebSite)
                                       .Set(x => x.Email, request.Email)
                                       .Set(x => x.RedeSocial, request.RedeSocial)
                                       .Set(x => x.Observacao, request.Observacao)
                                       .Set(x => x.Status, request.Status);

            var updateResult = await collection.ReplaceOneAsync(filter, request);

            if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
            {
                return request;
            }
            else if (updateResult.IsAcknowledged && updateResult.UpsertedId != null)
            {
                return request;
            }
        }

        return null;
    }
}