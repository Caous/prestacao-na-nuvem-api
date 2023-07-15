namespace SmartOficina.Api.Infrastructure.Repositories;

public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
{
    public ClienteRepository(OficinaContext context) : base(context)
    {
    }
}
