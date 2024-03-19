
namespace PrestacaoNuvem.Api.Domain.Services;

public class OrdemVendaService : IOrdemVendaService
{
    private readonly IOrdemVendaRepository _repository;
    private readonly IMapper _mapper;

    public OrdemVendaService(IOrdemVendaRepository ordemVendaRepository, IMapper mapper)
    {
        _repository = ordemVendaRepository;
        _mapper = mapper;
    }
    public Task ChangeStatus(Guid id, EOrdemVendaStatus status)
    {
        throw new NotImplementedException();
    }

    public Task<OrdemVendaDto> CreateOrdemVenda(OrdemVendaDto item)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<OrdemVendaDto> Desabled(Guid id, Guid userDesabled)
    {
        throw new NotImplementedException();
    }

    public Task<OrdemVendaDto> FindByIdOrdemVenda(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<OrdemVendaDto>> GetAllOrdemVenda(OrdemVendaDto item)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<OrdemVendaDto>> GetByOrdemVendaStatus(Guid prestadorId, ICollection<EOrdemVendaStatus> statusPrestacao)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<OrdemVendaDto>> GetByPrestador(Guid prestadorId)
    {
        throw new NotImplementedException();
    }

    public Task<OrdemVendaDto> UpdateOrdemVenda(OrdemVendaDto item)
    {
        throw new NotImplementedException();
    }
}
