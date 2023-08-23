namespace SmartOficina.Api.Infrastructure.Repositories.Services;

public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
{
    private readonly OficinaContext _context;
    public ClienteRepository(OficinaContext context) : base(context)
    {
        _context = context;
    }

    public async override Task<ICollection<Cliente>> GetAll(Guid id)
    {
        var result = await _context.Cliente.Where(x => x.PrestadorId == id).ToArrayAsync();
        await _context.DisposeAsync();
        return result;
    }
}
