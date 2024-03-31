
namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Services;

public class FilialRepository : GenericRepository<Filial>, IFilialRepository
{
    OficinaContext _context;
    public FilialRepository(OficinaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<ICollection<Filial>> GetAll(Guid id, Filial filter)
    {
        var result = await _context.Filial.Where(x => x.PrestadorId == id && x.DataDesativacao == null).ToArrayAsync();

        if (filter != null && result.Any())
        {
            if (!filter.Nome.IsMissing())
                result = result.Where(x => x.Nome == filter.Nome).ToArray();
            if (!filter.Logradouro.IsMissing())
                result = result.Where(x => x.Logradouro == filter.Logradouro).ToArray();
            
        }
        return result;
    }
}
