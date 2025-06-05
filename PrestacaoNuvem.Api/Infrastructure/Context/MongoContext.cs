using MongoDB.Driver;

namespace PrestacaoNuvem.Api.Infrastructure.Context;

public class LeadsMongoContext
{
    public IMongoDatabase Database { get; }

    public LeadsMongoContext(IMongoClient client)
    {
        Database = client.GetDatabase("leads");
    }
}

public class WhatsappMongoContext
{
    public IMongoDatabase Database { get; }

    public WhatsappMongoContext(IMongoClient client)
    {
        Database = client.GetDatabase("whatsapp");
    }
}
