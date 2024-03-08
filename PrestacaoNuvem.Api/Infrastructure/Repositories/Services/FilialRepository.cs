namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Services;

public class FilialRepository : GenericRepository<Filial>, IFilialRepository
{
    public FilialRepository(OficinaContext context) : base(context)
    {
    }
}
