using SmartOficina.Api.Domain.Model;
using SmartOficina.Api.Infrastructure.Repositories.Interfaces;

namespace SmartOficina.Api.Infrastructure.Repositories.Services;

public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
{
    public ClienteRepository(OficinaContext context) : base(context)
    {
    }
}
