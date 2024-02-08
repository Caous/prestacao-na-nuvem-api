namespace PrestacaoNuvem.Api.Domain.Services;

public class FuncionarioService : IFuncionarioService
{
    private readonly IFuncionarioPrestadorRepository _repositoryFuncionario;
    private readonly IMapper _mapper;

    public FuncionarioService(IFuncionarioPrestadorRepository repositoryFuncionario, IMapper mapper)
    {
        _repositoryFuncionario = repositoryFuncionario;
        _mapper = mapper;
    }

    public async Task<FuncionarioPrestadorDto> CreateFuncionario(FuncionarioPrestadorDto item)
    {
        var result = await _repositoryFuncionario.Create(_mapper.Map<FuncionarioPrestador>(item));

        return _mapper.Map<FuncionarioPrestadorDto>(result);
    }

    public async Task Delete(Guid id)
    {
        await _repositoryFuncionario.Delete(id);
    }

    public async Task<FuncionarioPrestadorDto> Desabled(Guid id, Guid userDesabled)
    {
        var result = await _repositoryFuncionario.Desabled(id, userDesabled);
        return _mapper.Map<FuncionarioPrestadorDto>(result);
    }

    public async Task<FuncionarioPrestadorDto> FindByIdFuncionario(Guid id)
    {
        var result = await _repositoryFuncionario.FindById(id);

        return _mapper.Map<FuncionarioPrestadorDto>(result);
    }

    public async Task<ICollection<FuncionarioPrestadorDto>> GetAllFuncionario(FuncionarioPrestadorDto item)
    {
        var result = await _repositoryFuncionario.GetAll(item.PrestadorId.Value, _mapper.Map<FuncionarioPrestador>(item));
        result = result.Where(x => x.PrestadorId == item.PrestadorId && x.DataDesativacao == null).ToList();
        return _mapper.Map<ICollection<FuncionarioPrestadorDto>>(result);
    }

    public async Task<FuncionarioPrestadorDto> UpdateFuncionario(FuncionarioPrestadorDto item)
    {
        var result = await _repositoryFuncionario.Update(_mapper.Map<FuncionarioPrestador>(item));

        return _mapper.Map<FuncionarioPrestadorDto>(result);
    }
}
