namespace SmartOficina.Api.Domain.Interfaces;

public interface IPrestacaoServicoService
{
    Task<ICollection<PrestacaoServicoDto>> GetAllPrestacaoServico(PrestacaoServicoDto item);
    Task<PrestacaoServicoDto> FindByIdPrestacaoServico(Guid Id);
    Task<PrestacaoServicoDto> CreatePrestacaoServico(PrestacaoServicoDto item);
    Task<PrestacaoServicoDto> UpdatePrestacaoServico(PrestacaoServicoDto item);
    Task Delete(Guid Id);
    Task<PrestacaoServicoDto> Desabled(Guid id, Guid userDesabled); 
    Task ChangeStatus(Guid id, EPrestacaoServicoStatus status);
    Task<ICollection<PrestacaoServicoDto>> GetByPrestador(Guid prestadorId);
    Task<ICollection<PrestacaoServicoDto>> GetByPrestacoesServicosStatus(Guid prestadorId, ICollection<EPrestacaoServicoStatus> statusPrestacao);
}
