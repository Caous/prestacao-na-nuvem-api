namespace SmartOficina.Api.Infrastructure.Repositories.Services;

public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
{
    public ClienteRepository(OficinaContext context) : base(context)
    {
    }
}
