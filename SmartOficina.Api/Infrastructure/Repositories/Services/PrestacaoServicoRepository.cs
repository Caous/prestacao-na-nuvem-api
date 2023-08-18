namespace SmartOficina.Api.Infrastructure.Repositories.Services;

public class PrestacaoServicoRepository : GenericRepository<PrestacaoServico>, IPrestacaoServicoRepository
{
    private readonly OficinaContext _context;

    public PrestacaoServicoRepository(OficinaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<PrestacaoServico> Create(PrestacaoServico item)
    {
        await _context.PrestacaoServico.AddAsync(item);

        await _context.SaveChangesAsync();

        await _context.DisposeAsync();

        return item;
    }

    public async Task<PrestacaoServico> FindById(Guid id)
    {
        var result = _context.PrestacaoServico
            .Where(f => f.Id == id)
            .Include(i => i.Prestador)
            .Include(i => i.Cliente)
            .Include(i => i.Veiculo)
            .Include(i => i.Produtos)
            .Include(i => i.Servicos)
                .ThenInclude(i => i.SubCategoriaServico)
                .ThenInclude(i => i.Categoria)
            .ToList();
        await _context.DisposeAsync();

        return result.FirstOrDefault();
    }

    public override async Task<PrestacaoServico> Update(PrestacaoServico item)
    {
        if (item != null && item.Servicos != null && item.Servicos.Any())
        {
            var servicoId = item.Servicos.Where(x => x.Id != Guid.Empty).Select(x => x.Id);
            var servicoASerDeletado = await _context.Servico.Where(x=> x.PrestacaoServicoId == item.Id && !servicoId.Contains(x.Id)).ToListAsync();

            foreach (var serv in servicoASerDeletado)
            {
                _context.Servico.Remove(serv);
            }

            foreach (var servico in item.Servicos)
            {
                servico.SubCategoriaServico = null;
               
                if (servico.Id == Guid.Empty)
                {
                    servico.PrestacaoServicoId = item.Id;
                    _context.Servico.Add(servico);
                }
                else
                {
                    servico.PrestacaoServicoId = item.Id;
                    _context.Servico.Update(servico);
                }
            }
        }

        if (item != null && item.Produtos != null && item.Produtos.Any())
        {
            var produtoId = item.Produtos.Where(x => x.Id != Guid.Empty).Select(x => x.Id);
            var produtosASerDeletado = await _context.Produto.Where(x => x.PrestacaoServicoId == item.Id && !produtoId.Contains(x.Id)).ToListAsync();

            foreach (var serv in produtosASerDeletado)
            {
                _context.Produto.Remove(serv);
            }

            foreach (var prod in item.Produtos)
            {
                if (prod.Id == Guid.Empty)
                {
                    prod.PrestacaoServicoId = item.Id;
                    _context.Produto.Add(prod);
                }
                else
                {
                    prod.PrestacaoServicoId = item.Id;
                    _context.Produto.Update(prod);
                }
            }
        }

        _context.PrestacaoServico.Update(item);
        await _context.SaveChangesAsync();
        await _context.DisposeAsync();

        return item;
    }

    public async Task ChangeStatus(Guid id, EPrestacaoServicoStatus status)
    {
        var prestacao = await _context.PrestacaoServico.FindAsync(id);
        if (prestacao is not null)
        {
            prestacao.Status = status;

            if (prestacao.Status == EPrestacaoServicoStatus.Concluido)
                prestacao.DataConclusaoServico = DateTime.Now;

            await _context.SaveChangesAsync();
            await _context.DisposeAsync();
        }
        else throw new Exception("Prestacao não encontrada");
    }

    public async Task<ICollection<PrestacaoServico>> GetByPrestador(Guid prestadorId)
    {
        var result = await _context.PrestacaoServico
            .Where(f => f.PrestadorId == prestadorId)
            .Include(i => i.Prestador)
            .Include(i => i.Cliente)
            .Include(i => i.Veiculo)
            .Include(i => i.Produtos)
            .Include(i => i.Servicos)
                .ThenInclude(i => i.SubCategoriaServico)
                .ThenInclude(i => i.Categoria)
            .ToArrayAsync();
        await _context.DisposeAsync();

        return result;
    }

    public async Task<ICollection<PrestacaoServico>> GetByPrestacoesServicosStatus(Guid prestadorId, ICollection<EPrestacaoServicoStatus> statusPrestacao)
    {
        var result = await _context.PrestacaoServico
            .Where(f => f.PrestadorId == prestadorId && statusPrestacao.Contains(f.Status))
            .Include(i => i.Prestador)
            .Include(i => i.Cliente)
            .Include(i => i.Veiculo)
            .Include(i => i.Produtos)
            .Include(i => i.Servicos)
                .ThenInclude(i => i.SubCategoriaServico)
                .ThenInclude(i => i.Categoria)
            .ToArrayAsync();
        await _context.DisposeAsync();

        return result;
    }
}
