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
        var result = await _repository.Create(_mapper.Map<Contrato>(item));

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
}
