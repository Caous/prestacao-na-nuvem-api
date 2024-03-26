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
        if (item != null && item.Produtos != null)
        {
            foreach (var produto in item.Produtos)
            {
                produto.PrestadorId = item.PrestadorId;
                ICollection<Produto> produtos = await _produtoRepository.GetAll(item.PrestadorId.Value, _mapper.Map<Produto>(produto));

                if (produtos != null)
                {
                    if (produto.Qtd < produtos.Count)
                        AtualizarQtdEstoqueMenos(DefinirQtdEstoqueAtual(produto.Qtd, produtos.Count), produtos);
                    if (produto.Qtd > produtos.Count)
                        AtualizarQtdEstoqueMaior(DefinirQtdEstoqueAtual(produto.Qtd, produtos.Count), produto);

                    await _produtoRepository.Update(_mapper.Map<Produto>(produto));
                }
            }
        }

        var result = await _repository.Create(_mapper.Map<OrdemVenda>(item));

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();

        return _mapper.Map<OrdemVendaDto>(result);
    }

    private void AtualizarQtdEstoqueMaior(int qtd, ProdutoDto item)
    {
        for (int i = 0; qtd < i; i--)
        {
            ProdutoDto itemCopy = ProdutoCopy(item);
            _produtoRepository.Create(_mapper.Map<Produto>(itemCopy));
        }
    }

    private static ProdutoDto ProdutoCopy(ProdutoDto item)
    {
        return new ProdutoDto() { Data_validade = item.Data_validade, Garantia = item.Garantia, Marca = item.Marca, Modelo = item.Modelo, Nome = item.Nome, PrestadorId = item.PrestadorId, Qtd = 1, TipoMedidaItem = item.TipoMedidaItem, UsrCadastro = item.UsrCadastro, Valor_Venda = item.Valor_Venda, Valor_Compra = item.Valor_Compra };
    }

    private static int DefinirQtdEstoqueAtual(int qtdAtual, int qtdEstoqueAntiga)
    {
        return qtdEstoqueAntiga - qtdAtual;
    }

    private async Task AtualizarQtdEstoqueMenos(int qtd, ICollection<Produto> item)
    {
        var itensEstoqueAtualizacao = item.Take(qtd);
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
