namespace PrestacaoNuvem.Api.Domain.Services;

public class PrestadorService : IPrestadorService
{
    private readonly IPrestadorRepository _repository;
    private readonly IMapper _mapper;

    public PrestadorService(IPrestadorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<PrestadorDto> CreatePrestador(PrestadorDto item)
    {
        var result = await _repository.Create(_mapper.Map<Prestador>(item));

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();

        return _mapper.Map<PrestadorDto>(result);
    }

    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();
    }

    public async Task<PrestadorDto> Desabled(Guid id, Guid userDesabled)
    {
        var result = await _repository.Desabled(id, userDesabled);

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();
        return _mapper.Map<PrestadorDto>(result);
    }

    public async Task<PrestadorDto> FindByIdPrestador(Guid id)
    {
        var result = await _repository.FindById(id);

        await _repository.DisposeCommitAsync();
        return _mapper.Map<PrestadorDto>(result);
    }

    public async Task<ICollection<PrestadorDto>> GetAllPrestador(PrestadorDto item)
    {
        var result = await _repository.GetAll(item.PrestadorId.Value, _mapper.Map<Prestador>(item));

        await _repository.DisposeCommitAsync();
        return _mapper.Map<ICollection<PrestadorDto>>(result);
    }

    public async Task<PrestadorDto> UpdatePrestador(PrestadorDto item)
    {
        var result = await _repository.Update(_mapper.Map<Prestador>(item));

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();
        return _mapper.Map<PrestadorDto>(result);
    }
}
