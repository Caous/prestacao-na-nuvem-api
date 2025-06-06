﻿namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Services;

public class PrestacaoServicoRepository : GenericRepository<PrestacaoServico>, IPrestacaoServicoRepository
{
    private readonly OficinaContext _context;

    public PrestacaoServicoRepository(OficinaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<ICollection<PrestacaoServico>> GetAll(Guid id, PrestacaoServico filter, bool admin)
    {
        var result = await _context.PrestacaoServico.Include(i => i.Prestador)
            .Include(i => i.Cliente)
            .Include(i => i.Veiculo)
            .Include(i => i.Produtos)
            .Include(i => i.Servicos)
                .ThenInclude(i => i.SubCategoriaServico)
                .ThenInclude(i => i.Categoria).ToArrayAsync();   
        return result;
    }

    public override async Task<PrestacaoServico> Create(PrestacaoServico item)
    {
        await _context.PrestacaoServico.AddAsync(item);
        return item;
    }

    public async Task<PrestacaoServico> FindById(Guid id)
    {
        var result = await _context.PrestacaoServico
            .Where(f => f.Id == id)
            .Include(i => i.Prestador)
            .Include(i => i.Cliente)
            .Include(i => i.Veiculo)
            .Include(i => i.Produtos)
            .Include(i => i.Servicos)
                .ThenInclude(i => i.SubCategoriaServico)
                .ThenInclude(i => i.Categoria)
            .FirstOrDefaultAsync();
        return result;
    }

    public override async Task<PrestacaoServico> Update(PrestacaoServico item)
    {
        if (item != null && item.Servicos != null && item.Servicos.Any())
        {
            var servicoId = item.Servicos.Where(x => x.Id != Guid.Empty).Select(x => x.Id);
            var servicoASerDeletado = await _context.Servico.Where(x => x.PrestacaoServicoId == item.Id && !servicoId.Contains(x.Id)).ToListAsync();

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

        return item;
    }

    public async Task ChangeStatus(PrestacaoServico prestacaoServico, EPrestacaoServicoStatus status)
    {
        if (prestacaoServico is not null)
        {
            prestacaoServico.Status = status;

            if (prestacaoServico.Status == EPrestacaoServicoStatus.Concluido)
                prestacaoServico.DataConclusaoServico = DateTime.Now;
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

        return result;
    }

    public async Task<ICollection<PrestacaoServico>> GetByPrestacoesServicosStatus(Guid prestadorId, ICollection<EPrestacaoServicoStatus> statusPrestacao)
    {
        var result = await _context.PrestacaoServico
            .Where(f =>  statusPrestacao.Contains(f.Status))
            .Include(i => i.Prestador)
            .Include(i => i.Cliente)
            .Include(i => i.Veiculo)
            .Include(i => i.Produtos)
            .Include(i => i.Servicos)
                .ThenInclude(i => i.SubCategoriaServico)
                .ThenInclude(i => i.Categoria)
            .ToArrayAsync();

        return result;
    }
}
