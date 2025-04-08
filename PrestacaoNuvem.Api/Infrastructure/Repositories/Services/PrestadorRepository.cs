
namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Services;

public class PrestadorRepository : GenericRepository<Prestador>, IPrestadorRepository
{
    OficinaContext _context;
    public PrestadorRepository(OficinaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<ICollection<Prestador>> GetAll(Guid id, Prestador filter, bool admin)
    {
        var result = await _context.Prestador.Where(x => x.DataDesativacao == null).ToArrayAsync();
        return result;
    }

}
