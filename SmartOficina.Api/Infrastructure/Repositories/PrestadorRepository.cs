namespace SmartOficina.Api.Infrastructure.Repositories;

public class PrestadorRepository : GenericRepository<Prestador>, IPrestadorRepository
{
    public PrestadorRepository(OficinaContext context) : base(context)
    {
    }

}
