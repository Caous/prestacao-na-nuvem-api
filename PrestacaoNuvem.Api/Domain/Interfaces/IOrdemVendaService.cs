namespace PrestacaoNuvem.Api.Domain.Interfaces;

public interface IOrdemVendaService
{
    Task<ICollection<OrdemVendaDto>> GetAllOrdemVenda(OrdemVendaDto item);
    Task<OrdemVendaDto> FindByIdOrdemVenda(Guid Id);
    Task<OrdemVendaDto> CreateOrdemVenda(OrdemVendaDto item);
    Task<OrdemVendaDto> UpdateOrdemVenda(OrdemVendaDto item);
    Task Delete(Guid Id);
    Task<OrdemVendaDto> Desabled(Guid id, Guid userDesabled);
    Task ChangeStatus(Guid id, EOrdemVendaStatus status);
    Task<ICollection<OrdemVendaDto>> GetByPrestador(Guid prestadorId);
    Task<ICollection<OrdemVendaDto>> GetByOrdemVendaStatus(Guid prestadorId, ICollection<EOrdemVendaStatus> statusPrestacao);
}
