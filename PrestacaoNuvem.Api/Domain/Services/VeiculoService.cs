namespace PrestacaoNuvem.Api.Domain.Services;

public class VeiculoService : IVeiculoService
{
    private readonly IVeiculoRepository _repository;
    private readonly IPrestacaoServicoRepository _prestacaoServicoRepository;
    private readonly IMapper _mapper;
    public VeiculoService(IVeiculoRepository repository, IMapper mapper, IPrestacaoServicoRepository prestacaoServico)
    {
        _repository = repository;
        _mapper = mapper;
        _prestacaoServicoRepository = prestacaoServico;
    }

    public async Task<VeiculoDto> CreateVeiculos(VeiculoDto item)
    {
        var result = await _repository.Create(_mapper.Map<Veiculo>(item));

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();

        return _mapper.Map<VeiculoDto>(result);
    }

    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();
    }

    public async Task<VeiculoDto> Desabled(Guid id, Guid userDesabled)
    {
        var result = await _repository.Desabled(id, userDesabled);

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();

        return _mapper.Map<VeiculoDto>(result);
    }

    public Task<VeiculoDto> FindByIdVeiculos(Guid Id)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<VeiculoDto>> GetAllCarCustumer(VeiculoDto item)
    {
        ICollection<Veiculo> returnCars = new List<Veiculo>();
        
        var result = await _prestacaoServicoRepository.GetByPrestador(item.PrestadorId.Value);

        var resultCar = result.Where(x => x.ClienteId == item.IdCliente).Select(x => x.Veiculo).GroupBy(x=> x.Placa);        
        
        foreach (var itemCar in resultCar)
            returnCars.Add(itemCar.First());
        
        await _repository.DisposeCommitAsync();

        return _mapper.Map<ICollection<VeiculoDto>>(returnCars);
    }

    public async Task<ICollection<VeiculoDto>> GetAllVeiculos(VeiculoDto item)
    {
        var result = await _repository.GetAll(item.PrestadorId.Value, _mapper.Map<Veiculo>(item), false);

        await _repository.DisposeCommitAsync();

        return _mapper.Map<ICollection<VeiculoDto>>(result);
    }

    public async Task<VeiculoDto> UpdateVeiculos(VeiculoDto item)
    {
        var result = await _repository.Update(_mapper.Map<Veiculo>(item));

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();
        return _mapper.Map<VeiculoDto>(result);
    }
}
