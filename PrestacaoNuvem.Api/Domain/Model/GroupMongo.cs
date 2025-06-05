using MongoDB.Bson;

namespace PrestacaoNuvem.Api.Domain.Model;

public class GroupMongo
{
    public ObjectId Id { get; set; }
    public string ConversationId { get; set; }
    public GroupMongoMessage[] Messages { get; set; } = Array.Empty<GroupMongoMessage>();
}
