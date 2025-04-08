namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Services;

public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
{
    private readonly OficinaContext _context;
    public ClienteRepository(OficinaContext context) : base(context)
    {
        _context = context;
    }

    public async override Task<ICollection<Cliente>> GetAll(Guid id, Cliente filter, bool admin)
    {
        Cliente[] result = [];
        if (admin)
            result = await _context.Cliente.ToArrayAsync();
        else
             result = await _context.Cliente.Where(x => x.PrestadorId == id && x.DataDesativacao == null).ToArrayAsync();


        if (filter != null && result.Any())
        {
            if (!filter.Nome.IsMissing())
                result = result.Where(x => x.Nome == filter.Nome).ToArray();
            if (!filter.CPF.IsMissing())
                result = result.Where(x => x.CPF == filter.CPF).ToArray();
            if (!filter.Email.IsMissing())
                result = result.Where(x => x.Email == filter.Email).ToArray();
        }
        return result;
    }
}
