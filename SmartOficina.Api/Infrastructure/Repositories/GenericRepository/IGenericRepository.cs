namespace SmartOficina.Api.Infrastructure.Repositories.GenericRepository;

public interface IGenericRepository<T> where T : class
{
    Task<ICollection<T>> GetAll(Guid id);
    Task<T> FindById(Guid Id);
    Task<T> Create(T item);
    Task<T> Update(T item);
    Task Delete(Guid Id);
    Task<T> Desabled(Guid id);

}