namespace SmartOficina.Api.Infrastructure.Repositories;

public class VeiculoRepository : GenericRepository<Veiculo>, IVeiculoRepository
{
    public VeiculoRepository(OficinaContext context) : base(context)
    {
    }

}
