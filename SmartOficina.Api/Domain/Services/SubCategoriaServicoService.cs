using SmartOficina.Api.Domain.Interfaces;

namespace SmartOficina.Api.Domain.Services;

public class SubCategoriaServicoService : ISubCategoriaServicoService
{
    private readonly ISubServicoRepository _repository;
    private readonly IMapper _mapper;
    public SubCategoriaServicoService(ISubServicoRepository subServicoRepository, IMapper mapper)
    {
        _repository = subServicoRepository;
        _mapper = mapper;
    }

    public async Task<SubCategoriaServicoDto> CreateSubCategoria(SubCategoriaServicoDto item)
    {
        var result = await _repository.Create(_mapper.Map<SubCategoriaServico>(item));

        return _mapper.Map<SubCategoriaServicoDto>(result);
    }

    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);
    }

    public async Task<SubCategoriaServicoDto> Desabled(Guid id, Guid userDesabled)
    {
        var resultFilter = await _repository.FindById(id);

        var result = await _repository.Desabled(id, userDesabled);
        return _mapper.Map<SubCategoriaServicoDto>(result);
    }

    public async Task<SubCategoriaServicoDto> FindByIdSubCategoria(Guid id)
    {
        var result = await _repository.FindById(id);

        return _mapper.Map<SubCategoriaServicoDto>(result);
    }

    public async Task<ICollection<SubCategoriaServicoDto>> GetAllSubCategoria(SubCategoriaServicoDto item)
    {
        var result = await _repository.GetAll(item.PrestadorId.Value, _mapper.Map<SubCategoriaServico>(item));

        return _mapper.Map<ICollection<SubCategoriaServicoDto>>(result);
    }

    public async Task<SubCategoriaServicoDto> UpdateSubCategoria(SubCategoriaServicoDto item)
    {

        var result = await _repository.Update(_mapper.Map<SubCategoriaServico>(item));

        return _mapper.Map<SubCategoriaServicoDto>(result);

    }
}
