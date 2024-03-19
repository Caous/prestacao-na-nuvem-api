using NuGet.Protocol.Core.Types;

namespace PrestacaoNuvem.Api.Domain.Services;

public class FuncionarioService : IFuncionarioService
{
    private readonly IFuncionarioPrestadorRepository _repository;
    private readonly IMapper _mapper;

    public FuncionarioService(IFuncionarioPrestadorRepository repositoryFuncionario, IMapper mapper)
    {
        _repository = repositoryFuncionario;
        _mapper = mapper;
    }

    public async Task<FuncionarioPrestadorDto> CreateFuncionario(FuncionarioPrestadorDto item)
    {
        var result = await _repository.Create(_mapper.Map<FuncionarioPrestador>(item));

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();


        return _mapper.Map<FuncionarioPrestadorDto>(result);
    }

    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();

    }

    public async Task<FuncionarioPrestadorDto> Desabled(Guid id, Guid userDesabled)
    {
        var result = await _repository.Desabled(id, userDesabled);

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();

        return _mapper.Map<FuncionarioPrestadorDto>(result);
    }

    public async Task<FuncionarioPrestadorDto> FindByIdFuncionario(Guid id)
    {
        var result = await _repository.FindById(id);

        await _repository.DisposeCommitAsync();

        return _mapper.Map<FuncionarioPrestadorDto>(result);
    }

    public async Task<ICollection<FuncionarioPrestadorDto>> GetAllFuncionario(FuncionarioPrestadorDto item)
    {
        var result = await _repository.GetAll(item.PrestadorId.Value, _mapper.Map<FuncionarioPrestador>(item));

        await _repository.DisposeCommitAsync();

        result = result.Where(x => x.PrestadorId == item.PrestadorId && x.DataDesativacao == null).ToList();
        

        return _mapper.Map<ICollection<FuncionarioPrestadorDto>>(result);
    }

    public async Task<FuncionarioPrestadorDto> UpdateFuncionario(FuncionarioPrestadorDto item)
    {
        var result = await _repository.Update(_mapper.Map<FuncionarioPrestador>(item));

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();

        return _mapper.Map<FuncionarioPrestadorDto>(result);
    }
}
