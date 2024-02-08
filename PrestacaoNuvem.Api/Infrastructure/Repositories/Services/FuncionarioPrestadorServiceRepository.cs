namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Services;

public class FuncionarioPrestadorServiceRepository : GenericRepository<FuncionarioPrestador>, IFuncionarioPrestadorRepository
{
    private readonly OficinaContext _context;
    public FuncionarioPrestadorServiceRepository(OficinaContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ICollection<FuncionarioPrestador>> GetListaFuncionarioPrestadorAsync(Guid id, FuncionarioPrestador filter)
    {
        var result = _context.FuncionarioPrestador
             .Where(f => f.PrestadorId == id && f.DataDesativacao == null)
             .Include(i => i.Prestador)
             .ToList();
        await _context.DisposeAsync();

        if (filter != null && result.Any())
        {
            if (!filter.Nome.IsMissing())
                result = result.Where(x => x.Nome == filter.Nome).ToList();
            if (!filter.CPF.IsMissing())
                result = result.Where(x => x.CPF == filter.CPF).ToList();
            if (!filter.Email.IsMissing())
                result = result.Where(x => x.Email == filter.Email).ToList();
        }
        return result;
    }
}
