using MongoDB.Driver;

namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Services;

public class MongoRepository : IMongoRepository
{
    private const string CollectionsExections = "executions";
    private const string CollectionsGroupMessages = "whatsapp";
    private readonly IMongoDatabase _dataBase;

    public MongoRepository(WhatsappMongoContext database)
    {
        _dataBase = database.Database;
    }

    public async Task<ICollection<GroupMongo>> GetAllAsync()
    {
        var collection = _dataBase.GetCollection<GroupMongo>(CollectionsGroupMessages);

        var group = await collection.Find(Builders<GroupMongo>.Filter.Empty).ToListAsync();

        return group;
    }
}
