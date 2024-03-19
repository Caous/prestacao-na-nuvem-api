namespace PrestacaoNuvem.Api.Domain.Services;

public class OrdemVendaService : IOrdemVendaService
{
    private readonly IOrdemVendaRepository _repository;
    private readonly IMapper _mapper;

    public OrdemVendaService(IOrdemVendaRepository ordemVendaRepository, IMapper mapper)
    {
        _repository = ordemVendaRepository;
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
        var result = await _repository.Create(_mapper.Map<OrdemVenda>(item));

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();

        return _mapper.Map<OrdemVendaDto>(result);
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
