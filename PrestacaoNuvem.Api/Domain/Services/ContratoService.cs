
namespace PrestacaoNuvem.Api.Domain.Services;

public class ContratoService : IContratoService
{
    private readonly IContratoRepository _repository;
    private readonly IMapper _mapper;

    public ContratoService(IContratoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ContratoDto> CreateContrato(ContratoDto item)
    {
        var clienteMap = _mapper.Map<Contrato>(item);
        var result = await _repository.Create(clienteMap);

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();

        return _mapper.Map<ContratoDto>(result);
    }

    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();
    }

    public async Task<ContratoDto> FindById(Guid id)
    {
        var result = await _repository.FindById(id);

        await _repository.DisposeCommitAsync();

        return _mapper.Map<ContratoDto>(result);
    }

    public async Task<ICollection<ContratoDto>> GetAll()
    {
        var result = await _repository.GetAll(new Guid(), new Contrato(), true);

        if (result == null || !result.Any())
            return null;

        return _mapper.Map<ICollection<ContratoDto>>(result);
    }
}
