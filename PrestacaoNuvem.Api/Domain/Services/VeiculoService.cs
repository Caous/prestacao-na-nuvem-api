namespace PrestacaoNuvem.Api.Domain.Services;

public class VeiculoService : IVeiculoService
{
    private readonly IVeiculoRepository _repository;
    private readonly IMapper _mapper;
    public VeiculoService(IVeiculoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<VeiculoDto> CreateVeiculos(VeiculoDto item)
    {
        var result = await _repository.Create(_mapper.Map<Veiculo>(item));

        return _mapper.Map<VeiculoDto>(result);
    }

    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);
    }

    public async Task<VeiculoDto> Desabled(Guid id, Guid userDesabled)
    {
        var result = await _repository.Desabled(id, userDesabled);

        return _mapper.Map<VeiculoDto>(result);
    }

    public Task<VeiculoDto> FindByIdVeiculos(Guid Id)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<VeiculoDto>> GetAllVeiculos(VeiculoDto item)
    {
        var result = await _repository.GetAll(item.PrestadorId.Value, _mapper.Map<Veiculo>(item));

        return _mapper.Map<ICollection<VeiculoDto>>(result);
    }

    public async Task<VeiculoDto> UpdateVeiculos(VeiculoDto item)
    {
        var result = await _repository.Update(_mapper.Map<Veiculo>(item));
        return _mapper.Map<VeiculoDto>(result);
    }
}
