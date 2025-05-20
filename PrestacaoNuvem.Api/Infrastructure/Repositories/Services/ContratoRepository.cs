
namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Services;

public class ContratoRepository : GenericRepository<Contrato>, IContratoRepository
{
    private readonly OficinaContext _context;
    public ContratoRepository(OficinaContext context) : base(context) => _context = context;

    public async override Task<ICollection<Contrato>> GetAll(Guid id, Contrato filter, bool admin)
    {
        Contrato[] result = [];
        //if (admin)
        result = await _context.Contrato.Include(x => x.Cliente).ToArrayAsync();
        //else
        //    result = await _context.Contrato.Include(x => x.Cliente).Where(x => x.PrestadorId == id && x.DataDesativacao == null).ToArrayAsync();


        if (filter != null && result.Any())
        {
            if (filter.ClienteId != null && filter.ClienteId != Guid.Empty)
                result = result.Where(x => x.ClienteId == filter.ClienteId).ToArray();
            if (filter.Status != null && filter.Status > 0)
                result = result.Where(x => x.Status == filter.Status).ToArray();
            if (filter.TipoContrato != null && filter.TipoContrato > 0)
                result = result.Where(x => x.TipoContrato == filter.TipoContrato).ToArray();
        }
        return result;
    }
}
