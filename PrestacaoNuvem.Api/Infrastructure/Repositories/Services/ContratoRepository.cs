namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Services;

public class ContratoRepository : GenericRepository<Contrato>, IContratoRepository
{
    public ContratoRepository(OficinaContext context) : base(context)
    {
    }
}
