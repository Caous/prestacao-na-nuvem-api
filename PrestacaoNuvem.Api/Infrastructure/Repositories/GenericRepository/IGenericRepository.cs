namespace PrestacaoNuvem.Api.Infrastructure.Repositories.GenericRepository;

public interface IGenericRepository<T> where T : class
{
    Task<ICollection<T>> GetAll(Guid id, T filter, bool admin);
    Task<T> FindById(Guid Id);
    Task<T> Create(T item);
    Task<T> Update(T item);
    Task Delete(Guid Id);
    Task<T> Desabled(Guid id, Guid userDesabled);
    Task CommitAsync();
    Task DisposeCommitAsync();

}