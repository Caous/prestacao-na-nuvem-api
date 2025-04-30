namespace PrestacaoNuvem.Api.Domain.Interfaces;

public interface IContratoService
{
    Task<ContratoDto> CreateContrato(ContratoDto item);
    Task Delete(Guid id);
    Task<ContratoDto> FindById(Guid id);
}
