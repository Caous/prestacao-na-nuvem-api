namespace PrestacaoNuvem.Api.Domain.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;
    private readonly IMapper _mapper;

    public ClienteService(IClienteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ClienteDto> CreateCliente(ClienteDto item)
    {
        var result = await _repository.Create(_mapper.Map<Cliente>(item));

        return _mapper.Map<ClienteDto>(result);
    }

    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);
    }

    public async Task<ClienteDto> Desabled(Guid id, Guid userDesabled)
    {
        var result = await _repository.Desabled(id, userDesabled);

        return _mapper.Map<ClienteDto>(result);
    }

    public async Task<ClienteDto> FindByIdCliente(Guid id)
    {
        var result = await _repository.FindById(id);

        return _mapper.Map<ClienteDto>(result);
    }

    public async Task<ICollection<ClienteDto>> GetAllCliente(ClienteDto item)
    {
        var result = await _repository.GetAll(item.PrestadorId.Value, _mapper.Map<Cliente>(item));

        return _mapper.Map<ICollection<ClienteDto>>(result);
    }

    public async Task<ClienteDto> UpdateCliente(ClienteDto item)
    {
        var result = await _repository.Update(_mapper.Map<Cliente>(item));

        return _mapper.Map<ClienteDto>(result);
    }
}
