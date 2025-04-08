
using Azure.Core;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2016.Excel;

namespace PrestacaoNuvem.Api.Domain.Services;

public class FilialService : IFilialService
{
    private readonly IFilialRepository _repository;
    private readonly IMapper _mapper;

    public FilialService(IFilialRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<FilialDto> CreateFilial(FilialDto request)
    {
        var result = await _repository.Create(_mapper.Map<Filial>(request));

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();

        return _mapper.Map<FilialDto>(result);
    }

    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);
        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();
    }

    public async Task<FilialDto> Desabled(Guid id, Guid userDesabled)
    {
        var result = await _repository.Desabled(id, userDesabled);

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();

        return _mapper.Map<FilialDto>(result);
    }

    public async Task<FilialDto> FindByIdFilial(Guid id)
    {
        var result = await _repository.FindById(id);

        await _repository.DisposeCommitAsync();

        return _mapper.Map<FilialDto>(result);
    }

    public async Task<ICollection<FilialDto>> GetAllFilial(FilialDto request)
    {
        var result = await _repository.GetAll(request.PrestadorId.Value, _mapper.Map<Filial>(request), false);

        await _repository.DisposeCommitAsync();

        return _mapper.Map<ICollection<FilialDto>>(result);
    }

    public async Task<FilialDto> UpdateFilial(FilialDto request)
    {
        var result = await _repository.Update(_mapper.Map<Filial>(request));

        await _repository.CommitAsync();
        await _repository.DisposeCommitAsync();


        return _mapper.Map<FilialDto>(result);
    }
}
