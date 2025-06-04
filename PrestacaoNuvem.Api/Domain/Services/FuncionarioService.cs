using Microsoft.AspNetCore.Mvc.TagHelpers;
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
        RemoveMaskRequest(item);
        var employee = _mapper.Map<FuncionarioPrestador>(item);
        var result = await _repository.Create(employee);

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

    public async Task<ICollection<FuncionarioPrestadorDto>> GetAllFuncionario(FuncionarioPrestadorDto item, bool isAdmin)
    {
        var result = await _repository.GetAll(item.PrestadorId.Value, _mapper.Map<FuncionarioPrestador>(item), isAdmin);

        await _repository.DisposeCommitAsync();

        result = result.Where(x => x.PrestadorId == item.PrestadorId && x.DataDesativacao == null).ToList();


        return _mapper.Map<ICollection<FuncionarioPrestadorDto>>(result);
    }

    public async Task<FuncionarioPrestadorDto> UpdateFuncionario(FuncionarioPrestadorDto item)
    {
        RemoveMaskRequest(item);
        var employee = _mapper.Map<FuncionarioPrestador>(item);
        var result = await _repository.Update(employee);

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();

        return _mapper.Map<FuncionarioPrestadorDto>(result);
    }

    private static void RemoveMaskRequest(FuncionarioPrestadorDto mapperRequest)
    {
        mapperRequest.CPF = FormatHelper.RemoveMask(mapperRequest.CPF);
        mapperRequest.RG = FormatHelper.RemoveMask(mapperRequest.RG);
        mapperRequest.Telefone = string.IsNullOrWhiteSpace(mapperRequest.Telefone)
        ? string.Empty
            : FormatHelper.RemoveMask(mapperRequest.Telefone);


    }
}
