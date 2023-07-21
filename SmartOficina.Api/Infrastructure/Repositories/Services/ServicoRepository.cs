namespace SmartOficina.Api.Infrastructure.Repositories.Services;

public class ServicoRepository : GenericRepository<Servico>, IServicoRepository
{
    public ServicoRepository(OficinaContext context) : base(context)
    {
    }

}
