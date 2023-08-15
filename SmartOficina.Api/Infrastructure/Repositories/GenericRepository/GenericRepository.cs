namespace SmartOficina.Api.Infrastructure.Repositories.GenericRepository;

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

    public async Task<T> Desabled(Guid id)
    {
        T item = await _context.Set<T>().FindAsync(id);

        if (item is null)
            throw new Exception("Indice não encontrado");

        //item.GetType().GetProperty("Ativo").SetValue(Boolean,true);
        //item.GetType().GetProperty("Dt_Desativação").SetValue(DateTime, DateTime.Now);


        _context.Set<T>().Update(item);
        await _context.SaveChangesAsync();
        await _context.DisposeAsync();

        return item;

    }

    public async Task<T> FindById(Guid Id) =>  await _context.Set<T>().FindAsync(Id);   


    public async Task<ICollection<T>> GetAll()
    {
        var result = await _context.Set<T>().ToListAsync();
        await _context.DisposeAsync();

        return result;
    }

    public async Task<T> Update(T item)
    {
        _context.Set<T>().Update(item);
        await _context.SaveChangesAsync();
        await _context.DisposeAsync();
        return item;
    }
}
