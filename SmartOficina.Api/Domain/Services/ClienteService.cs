using SmartOficina.Api.Domain.Interfaces;

namespace SmartOficina.Api.Domain.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;
    private readonly IMapper _mapper;

    public ClienteService(IClienteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ClienteDto> CreateProduto(ClienteDto item)
    {
        var result = await _repository.Create(_mapper.Map<Cliente>(item));

        return _mapper.Map<ClienteDto>(result);
    }

    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);
    }

    public async Task<ClienteDto> Desabled(Guid id)
    {
        var result = await _repository.Desabled(id);

        return _mapper.Map<ClienteDto>(result);
    }

    public async Task<ClienteDto> FindByIdProduto(Guid id)
    {
        var result = await _repository.FindById(id);

        return _mapper.Map<ClienteDto>(result);
    }

    public async Task<ICollection<ClienteDto>> GetAllProduto(ClienteDto item)
    {
        var result = await _repository.GetAll(item.PrestadorId.Value, _mapper.Map<Cliente>(item));

        return _mapper.Map<ICollection<ClienteDto>>(result);
    }

    public async Task<ClienteDto> UpdateProduto(ClienteDto item)
    {
        var result = await _repository.Update(_mapper.Map<Cliente>(item));

        return _mapper.Map<ClienteDto>(result);
    }
}
