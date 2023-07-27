namespace SmartOficina.Api.Infrastructure.Repositories.Services
{
    public class FuncionarioPrestadorServiceRepository : GenericRepository<FuncionarioPrestador>, IFuncionarioPrestadorRepository
    {
        public FuncionarioPrestadorServiceRepository(OficinaContext context) : base(context)
        {
        }
    }
}
