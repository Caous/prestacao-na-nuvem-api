namespace SmartOficina.Api.Infrastructure.Repositories.Services;

public class VeiculoRepository : GenericRepository<Veiculo>, IVeiculoRepository
{
    public VeiculoRepository(OficinaContext context) : base(context)
    {
    }

}
