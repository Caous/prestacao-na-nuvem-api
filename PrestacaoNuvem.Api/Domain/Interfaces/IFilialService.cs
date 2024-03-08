namespace PrestacaoNuvem.Api.Domain.Interfaces;

public interface IFilialService
{
    Task<ICollection<FilialDto>> GetAllFilial(FilialDto request);
    Task<FilialDto> FindByIdFilial(Guid Id);
    Task<FilialDto> CreateFilial(FilialDto request);
    Task<FilialDto> UpdateFilial(FilialDto request);
    Task Delete(Guid Id);
    Task<FilialDto> Desabled(Guid id, Guid userDesabled);
}
