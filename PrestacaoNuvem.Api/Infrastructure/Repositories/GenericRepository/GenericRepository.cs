namespace PrestacaoNuvem.Api.Infrastructure.Repositories.GenericRepository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly OficinaContext _context;

    public GenericRepository(OficinaContext context)
    {
        _context = context;
    }

    public virtual async Task<T> Create(T item)
    {
        await _context.Set<T>().AddAsync(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task Delete(Guid Id)
    {
        T item = await _context.Set<T>().FindAsync(Id);

        if (item is null)
            throw new Exception("Indice não encontrado");

        _context.Set<T>().Remove(item);
        await _context.SaveChangesAsync();
        await _context.DisposeAsync();

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
        await _context.SaveChangesAsync();
        await _context.DisposeAsync();

        return item;

    }

    public async Task<T> FindById(Guid Id) => await _context.Set<T>().FindAsync(Id);


    public virtual async Task<ICollection<T>> GetAll(Guid id, T filter)
    {
        var result = await _context.Set<T>().ToListAsync();
        await _context.DisposeAsync();

        return result;
    }

    public virtual async Task<T> Update(T item)
    {
        _context.Set<T>().Update(item);
        await _context.SaveChangesAsync();
        await _context.DisposeAsync();
        return item;
    }
}
