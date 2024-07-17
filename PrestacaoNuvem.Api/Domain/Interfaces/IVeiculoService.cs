namespace PrestacaoNuvem.Api.Domain.Interfaces;

public interface IVeiculoService
{
    Task<ICollection<VeiculoDto>> GetAllVeiculos(VeiculoDto item);
    Task<VeiculoDto> FindByIdVeiculos(Guid Id);
    Task<VeiculoDto> CreateVeiculos(VeiculoDto item);
    Task<VeiculoDto> UpdateVeiculos(VeiculoDto item);
    Task Delete(Guid Id);
    Task<VeiculoDto> Desabled(Guid id, Guid userDesabled);
    Task<ICollection<VeiculoDto>> GetAllCarCustumer(VeiculoDto item);
}
