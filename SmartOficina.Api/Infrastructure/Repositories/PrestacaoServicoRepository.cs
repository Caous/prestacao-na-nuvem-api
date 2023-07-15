namespace SmartOficina.Api.Infrastructure.Repositories;

public class PrestacaoServicoRepository : GenericRepository<PrestacaoServico>, IPrestacaoServicoRepository
{
    private readonly OficinaContext _context;
    public PrestacaoServicoRepository(OficinaContext context) : base(context)
    {
        _context = context;
    }
    public async Task<PrestacaoServico> Add(PrestacaoServico prestacaoServico)
    {
        prestacaoServico.Status = PrestacaoServicoStatus.Aberto;

        _context.PrestacaoServico.Add(prestacaoServico);
        await _context.SaveChangesAsync();
        await _context.DisposeAsync();

        return prestacaoServico;
    }

    public async Task ChangeStatus(Guid id, PrestacaoServicoStatus status)
    {
        var prestacao = await _context.PrestacaoServico.FindAsync(id);
        if (prestacao is not null)
        {
            prestacao.Status = status;
            await _context.SaveChangesAsync();
            await _context.DisposeAsync();
        }
        else throw new Exception("Prestacao não encontrada");
    }

    public async Task<ICollection<PrestacaoServico>> GetByPrestador(Guid prestadorId)
    {
        var result =  await _context.PrestacaoServico
            .Where(f => f.PrestadorId == prestadorId)
            .Include(i => i.Prestador)
            .Include(i => i.Cliente)
            .Include(i => i.Servicos)
            .ToArrayAsync();
        await _context.DisposeAsync();

        return result;
    }
}
