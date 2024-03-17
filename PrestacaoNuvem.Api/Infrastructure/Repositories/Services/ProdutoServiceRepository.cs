namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Services;
public class ProdutoRepository : GenericRepository<Produto>, IProdutoRepository
{
    private readonly OficinaContext _context;
    public ProdutoRepository(OficinaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<ICollection<Produto>> GetAll(Guid id, Produto filter)
    {
        var result = await _context.Produto.Where(x => x.PrestacaoServicoId == null && x.PrestadorId == id && x.DataDesativacao == null).ToArrayAsync();
       

        if (filter != null && result.Any())
        {
            if (!filter.Nome.IsMissing())
                result = result.Where(x => x.Nome == filter.Nome).ToArray();
            if (!filter.Marca.IsMissing())
                result = result.Where(x => x.Marca == filter.Marca).ToArray();
            if (!filter.Modelo.IsMissing())
                result = result.Where(x => x.Modelo== filter.Modelo).ToArray();
        }
        return result;
    }
}
