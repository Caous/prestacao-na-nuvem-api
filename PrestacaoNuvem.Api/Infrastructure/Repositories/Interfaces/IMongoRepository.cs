namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Interfaces;

public interface IMongoRepository
{
    Task<ICollection<GroupMongo>> GetAllAsync();
}
