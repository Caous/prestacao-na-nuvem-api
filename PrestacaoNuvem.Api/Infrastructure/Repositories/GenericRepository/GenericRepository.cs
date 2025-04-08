namespace PrestacaoNuvem.Api.Infrastructure.Repositories.GenericRepository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly OficinaContext _context;

    public GenericRepository(OficinaContext context)
    {
        _context = context;
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task DisposeCommitAsync()
    {
        await _context.DisposeAsync();
    }

    public virtual async Task<T> Create(T item)
    {
        await _context.Set<T>().AddAsync(item);
        return item;
    }

    public async Task Delete(Guid Id)
    {
        T item = await _context.Set<T>().FindAsync(Id);

        if (item is null)
            throw new Exception("Indice não encontrado");

        _context.Set<T>().Remove(item);
    }

    public async Task<T> Desabled(Guid id, Guid userDesabled)
    {
        T item = await _context.Set<T>().FindAsync(id);

        if (item is null)
            throw new Exception("Indice não encontrado");

        PropertyInfo nameprop = typeof(T).GetProperty("DataDesativacao");

        if (nameprop != null)
            nameprop.SetValue(item, DateTime.Now);


        _context.Set<T>().Update(item);

        return item;

    }

    public async Task<T> FindById(Guid Id) => await _context.Set<T>().FindAsync(Id);


    public virtual async Task<ICollection<T>> GetAll(Guid id, T filter, bool admin)
    {
        var result = await _context.Set<T>().ToListAsync();
        return result;
    }

    public virtual async Task<T> Update(T item)
    {
        _context.Set<T>().Update(item);
        return item;
    }
}
