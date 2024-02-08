namespace PrestacaoNuvem.Api.Domain.Interfaces;

public interface IPrestadorService
{
    Task<ICollection<PrestadorDto>> GetAllPrestador(PrestadorDto item);
    Task<PrestadorDto> FindByIdPrestador(Guid Id);
    Task<PrestadorDto> CreatePrestador(PrestadorDto item);
    Task<PrestadorDto> UpdatePrestador(PrestadorDto item);
    Task Delete(Guid Id);
    Task<PrestadorDto> Desabled(Guid id, Guid userDesabled);
}
