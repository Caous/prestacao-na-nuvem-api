using ClosedXML.Excel;
using PrestacaoNuvem.Api.Enumerations;

namespace PrestacaoNuvem.Api.Domain.Services;

public class ProdutoService : IProdutoService
{
    private readonly IMapper _mapper;
    private readonly IProdutoRepository _repository;

    public ProdutoService(IMapper mapper, IProdutoRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ProdutoDto> CreateProduto(ProdutoDto item)
    {
        for (int i = 0; i < item.Qtd; i++)
            await _repository.Create(_mapper.Map<Produto>(item));


        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();

        return _mapper.Map<ProdutoDto>(item);
    }

    public async Task<string> CreateProdutoLot(ICollection<ProdutoDto> itens)
    {
        foreach (var item in itens)
            for (int i = 0; i < item.Qtd; i++)
                await _repository.Create(_mapper.Map<Produto>(item));

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();
        return "Produtos cadastrados com sucesso qtd de produtos cadastrados: " + itens.Count();
    }

    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);
        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();
    }

    public async Task<ProdutoDto> Desabled(Guid id, Guid userDesabled)
    {

        var result = await _repository.Desabled(id, userDesabled);

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();
        return _mapper.Map<ProdutoDto>(result);
    }

    public async Task<ProdutoDto> FindByIdProduto(Guid id)
    {
        var result = await _repository.FindById(id);

        await _repository.DisposeCommitAsync();
        return _mapper.Map<ProdutoDto>(result);
    }

    public async Task<ICollection<ProdutoDto>> GetAllProduto(ProdutoDto item)
    {
        var result = await _repository.GetAll(item.PrestadorId.Value, _mapper.Map<Produto>(item));
        
        await _repository.DisposeCommitAsync();
        
        return _mapper.Map<ICollection<ProdutoDto>>(result);
    }

    public async Task<ICollection<ProdutoDto>?> MapperProdutoLot(IFormFile file)
    {
        List<ProdutoDto> produtosLot = new();
        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);

            using (var xls = new XLWorkbook(stream))
            {
                var planilha = xls.Worksheets.First();

                for (int l = 2; l <= planilha.Rows().Count(); l++)
                {
                    string tituloProduto = planilha.Cell($"A{l}").Value.ToString();
                    if (tituloProduto.IsMissing())
                        return null;

                    string marcProduto = planilha.Cell($"B{l}").Value.ToString();
                    if (marcProduto.IsMissing())
                        return null;

                    int qtd = Convert.ToInt32(planilha.Cell($"C{l}").Value.ToString());
                    if (qtd < 0)
                        return null;

                    float valCompra = (float)planilha.Cell($"D{l}").Value;
                    if (valCompra < 0)
                        return null;

                    float valVenda = (float)planilha.Cell($"E{l}").Value;
                    if (valVenda < 0)
                        return null;

                    string modelo = planilha.Cell($"G{l}").Value.ToString();
                    if (modelo.IsMissing())
                        return null;

                    ETipoMedidaItemDto tipoItem = ETipoMedidaItemDto.Produto;


                    int tipoItemCompare = Convert.ToInt32(planilha.Cell($"F{l}").Value.ToString());

                    switch (tipoItemCompare)
                    {
                        case 0:
                            tipoItem = ETipoMedidaItemDto.Litro;
                            break;
                        case 1:
                            tipoItem = ETipoMedidaItemDto.Peca;
                            break;
                        case 2:
                            tipoItem = ETipoMedidaItemDto.Produto;
                            break;
                        default:
                            tipoItem = ETipoMedidaItemDto.Produto;
                            break;
                    }

                    produtosLot.Add(new ProdutoDto { Nome = tituloProduto, Marca = marcProduto, Valor_Compra = valCompra, Valor_Venda = valVenda, Qtd = qtd, TipoMedidaItem = tipoItem, Modelo = modelo });
                }
            }
        }
        return produtosLot;

    }

    public async Task<ProdutoDto> UpdateProduto(ProdutoDto item)
    {
        var result = await TratarUpdate(item);

        return _mapper.Map<ProdutoDto>(result);
    }

    private async Task<ProdutoDto> TratarUpdate(ProdutoDto item)
    {
        ICollection<Produto> produtos =  await _repository.GetAll(item.PrestadorId.Value, _mapper.Map<Produto>(item));


        if (produtos != null)
        {

            if (item.Qtd < produtos.Count)
                AtualizarQtdEstoqueMenos(DefinirQtdEstoqueAtual(item.Qtd, produtos.Count), produtos);
            if (item.Qtd > produtos.Count)
                AtualizarQtdEstoqueMaior(DefinirQtdEstoqueAtual(item.Qtd, produtos.Count), item);


            foreach (var produto in produtos)
                await _repository.Update(produto);
        }


        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();

        return item;
    }

    private void AtualizarQtdEstoqueMaior(int qtd, ProdutoDto item)
    {
        for (int i = 0; qtd < i; i--)
        {
            ProdutoDto itemCopy = ProdutoCopy(item);
            _repository.Create(_mapper.Map<Produto>(itemCopy));
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

    public async Task<ICollection<ProdutoDto>> GetAllGroupByProduto(ProdutoDto item)
    {
        var result = await _repository.GetAll(item.PrestadorId.Value, _mapper.Map<Produto>(item));

        await _repository.DisposeCommitAsync();

        if (result == null)
            return new List<ProdutoDto>();

        var resultAgrupado = result.GroupBy(x => x.Nome).Select(x =>
            new ProdutoDto
            {
                Id = x.First().Id,
                Nome = x.First().Nome,
                Marca = x.First().Marca,
                DataCadastro = x.First().DataCadastro,
                Data_validade = x.First().Data_validade,
                Modelo = x.First().Modelo,
                Garantia = x.First().Garantia,
                Qtd = x.Count(),
                TipoMedidaItem = (ETipoMedidaItemDto)x.First().TipoMedidaItem,
                PrestadorId = x.First().PrestadorId,
                UsrCadastro = x.First().UsrCadastro,
                Valor_Venda = x.First().Valor_Venda,
                Valor_Compra = x.First().Valor_Compra
            }
        );

        return resultAgrupado.ToList();
    }

    public async Task<ProdutoDto> GetProdutoInfo(ProdutoDto item)
    {
        var result = await GetAllGroupByProduto(item);

        await _repository.DisposeCommitAsync();

        return _mapper.Map<ProdutoDto>(result.First());
    }
}
