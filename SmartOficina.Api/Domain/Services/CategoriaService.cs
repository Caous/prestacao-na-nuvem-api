using SmartOficina.Api.Domain.Interfaces;

namespace SmartOficina.Api.Domain.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaServicoRepository _repository;
    private readonly IMapper _mapper;
    public CategoriaService(ICategoriaServicoRepository categoriaServicoRepository, IMapper mapper)
    {
        _repository = categoriaServicoRepository;
        _mapper = mapper;
    }

    public async Task<CategoriaServicoDto> CreateCategoria(CategoriaServicoDto item)
    {
        var result = await _repository.Create(_mapper.Map<CategoriaServico>(item));
        return _mapper.Map<CategoriaServicoDto>(result);
    }

    public async Task Delete(Guid Id)
    {
        await _repository.Delete(Id);
    }

    public Task<CategoriaServicoDto> Desabled(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<CategoriaServicoDto> FindByIdCategoria(Guid Id)
    {
        var result = await _repository.FindById(Id);

        return _mapper.Map<CategoriaServicoDto>(result);
    }

    public async Task<ICollection<CategoriaServicoDto>> GetAllCategoria(CategoriaServicoDto item)
    {
        var result = await _repository.GetAll(item.PrestadorId.Value, _mapper.Map<CategoriaServico>(item));

        return _mapper.Map<ICollection<CategoriaServicoDto>>(result);
    }

    public async Task<CategoriaServicoDto> UpdateCategoria(CategoriaServicoDto item)
    {
        var result = await _repository.Update(_mapper.Map<CategoriaServico>(item));

        return _mapper.Map<CategoriaServicoDto>(result);
    }
}
