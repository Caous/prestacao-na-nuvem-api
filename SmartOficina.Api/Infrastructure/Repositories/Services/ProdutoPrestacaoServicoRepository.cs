namespace SmartOficina.Api.Infrastructure.Repositories.Services;

public class ProdutoPrestacaoServicoRepository : GenericRepository<ProdutoPrestacaoServico>, IProdutoPrestacaoServicoRepository
{
    public ProdutoPrestacaoServicoRepository(OficinaContext context) : base(context)
    {
    }
}
