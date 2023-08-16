namespace SmartOficina.Api.Infrastructure.Repositories.Services;

public class PrestadorRepository : GenericRepository<Prestador>, IPrestadorRepository
{
    public PrestadorRepository(OficinaContext context) : base(context)
    {
    }


}
