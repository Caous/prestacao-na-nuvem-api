using NuGet.Packaging;
using PrestacaoNuvem.Api.Domain.Model;

namespace PrestacaoNuvem.Api.Domain.Services;

public class OrdemVendaService : IOrdemVendaService
{
    private readonly IOrdemVendaRepository _repository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;

    public OrdemVendaService(IOrdemVendaRepository ordemVendaRepository, IProdutoRepository produtoRepository, IMapper mapper)
    {
        _repository = ordemVendaRepository;
        _produtoRepository = produtoRepository;
        _mapper = mapper;
    }
    public async Task ChangeStatus(Guid id, EOrdemVendaStatus status)
    {
        var ordemVenda = await _repository.FindById(id);

        var alterandoStatusTask = _repository.ChangeStatus(ordemVenda, status);

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();

        Task.WaitAll(alterandoStatusTask);
    }

    public async Task<OrdemVendaDto> CreateOrdemVenda(OrdemVendaDto item)
    {
        OrdemVenda ordemVendaNovo = _mapper.Map<OrdemVenda>(item);
        ICollection<Produto> produtosParaRemover = new List<Produto>();
        ICollection<Produto> produtosParaAdicionar = new List<Produto>();

        if (ordemVendaNovo != null && ordemVendaNovo.Produtos != null)
        {
            for (int i = 0; i < ordemVendaNovo.Produtos.Count; i++)
            {
                var produto = ordemVendaNovo.Produtos.ElementAt(i);
                var produtoDto = item.Produtos.ElementAt(i);
                produto.PrestadorId = ordemVendaNovo.PrestadorId;
                ICollection<Produto> produtos = await _produtoRepository.GetAll(ordemVendaNovo.PrestadorId, produto);

                if (produtos != null && produtos.Any())
                {
                    AtualizarQtdEstoqueMenor(produtoDto.Qtd, produtos);

                    produtosParaRemover.Add(produto);

                    foreach (var produtoEstoque in produtos.Where(x=> x.DataDesativacao != null))
                        produtosParaAdicionar.Add(produtoEstoque);
                }
                else
                {
                    produto.UsrCadastro = item.UsrCadastro.Value;
                    produto.PrestadorId = item.PrestadorId.Value;

                    for (int j = 1; j <= produtoDto.Qtd; j++)
                    {
                        if (j == produtoDto.Qtd)                        
                            break;
                        
                        produtosParaAdicionar.Add(produto);
                    }
                }
            }

        }

        if (produtosParaRemover.Any())
        {
            foreach (var produto in produtosParaRemover)
                ordemVendaNovo.Produtos.Remove(produto);
        }

        if (produtosParaAdicionar.Any())
        {
            foreach (var produto in produtosParaAdicionar)
                ordemVendaNovo.Produtos.Add(produto);
        }

        ordemVendaNovo.Status = EOrdemVendaStatus.Concluido;

        var result = await _repository.Create(ordemVendaNovo);

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();

        return _mapper.Map<OrdemVendaDto>(result);
    }


    private void AtualizarQtdEstoqueMenor(int qtd, ICollection<Produto> item)
    {
        var itensEstoqueAtualizacao = item.Take(qtd == 0 ? 1 : qtd);
        foreach (var itemAtualizacao in itensEstoqueAtualizacao)
        {
            itemAtualizacao.DataDesativacao = DateTime.Now;
            itemAtualizacao.UsrDesativacao = item.FirstOrDefault().UsrCadastro;
        }
    }

    public async Task Delete(Guid Id)
    {
        await _repository.Delete(Id);

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();
    }

    public async Task<OrdemVendaDto> Desabled(Guid id, Guid userDesabled)
    {
        var result = await _repository.Desabled(id, userDesabled);

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();

        return _mapper.Map<OrdemVendaDto>(result);
    }

    public async Task<OrdemVendaDto> FindByIdOrdemVenda(Guid Id)
    {
        var result = await _repository.FindById(Id);

        await _repository.DisposeCommitAsync();

        return _mapper.Map<OrdemVendaDto>(result);
    }

    public async Task<ICollection<OrdemVendaDto>> GetAllOrdemVenda(OrdemVendaDto item)
    {
        var result = await _repository.GetAll(item.PrestadorId.Value, _mapper.Map<OrdemVenda>(item));
        await _repository.DisposeCommitAsync();
        return _mapper.Map<ICollection<OrdemVendaDto>>(result);
    }

    public async Task<ICollection<OrdemVendaDto>> GetByOrdemVendaStatus(Guid prestadorId, ICollection<EOrdemVendaStatus> statusPrestacao)
    {
        var result = await _repository.GetByPrestacoesServicosStatus(prestadorId, statusPrestacao);

        await _repository.DisposeCommitAsync();

        return _mapper.Map<ICollection<OrdemVendaDto>>(result);
    }

    public async Task<ICollection<OrdemVendaDto>> GetByPrestador(Guid prestadorId)
    {
        var result = await _repository.GetByPrestador(prestadorId);

        await _repository.DisposeCommitAsync();

        return _mapper.Map<ICollection<OrdemVendaDto>>(result);
    }

    public async Task<OrdemVendaDto> UpdateOrdemVenda(OrdemVendaDto item)
    {
        var result = await _repository.Update(_mapper.Map<OrdemVenda>(item));

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();

        return _mapper.Map<OrdemVendaDto>(result);
    }
}
