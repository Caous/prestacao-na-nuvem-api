namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Services;

public class VeiculoRepository : GenericRepository<Veiculo>, IVeiculoRepository
{
    private readonly OficinaContext _context;
    public VeiculoRepository(OficinaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<ICollection<Veiculo>> GetAll(Guid id, Veiculo filter)
    {
        var result = await _context.Veiculo.Where(x => x.PrestadorId == id && x.DataDesativacao == null).ToArrayAsync();
        await _context.DisposeAsync();
        return result;
    }

}
