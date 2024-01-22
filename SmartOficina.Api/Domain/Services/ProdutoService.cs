namespace SmartOficina.Api.Domain.Services;

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
        var result = await _repository.Create(_mapper.Map<Produto>(item));

        return _mapper.Map<ProdutoDto>(result);
    }

    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);
    }

    public async Task<ProdutoDto> Desabled(Guid id, Guid userDesabled)
    {

        var result = await _repository.Desabled(id, userDesabled);

        return _mapper.Map<ProdutoDto>(result);
    }

    public async Task<ProdutoDto> FindByIdProduto(Guid id)
    {
        var result = await _repository.FindById(id);

        return _mapper.Map<ProdutoDto>(result);
    }

    public async Task<ICollection<ProdutoDto>> GetAllProduto(ProdutoDto item)
    {
        var result = await _repository.GetAll(item.PrestadorId.Value, _mapper.Map<Produto>(item));

         return _mapper.Map<ICollection<ProdutoDto>>(result);
    }

    public async Task<ProdutoDto> UpdateProduto(ProdutoDto item)
    {
        var result = await _repository.Update(_mapper.Map<Produto>(item));

        return _mapper.Map<ProdutoDto>(result);
    }
}
