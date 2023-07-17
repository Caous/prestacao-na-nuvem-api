using SmartOficina.Api.Domain.Model;

namespace SmartOficina.Api.Infrastructure.Repositories.Services;

public class SubServicoRepository : GenericRepository<SubServico>, ISubServicoRepository
{
    public SubServicoRepository(OficinaContext context) : base(context)
    {
    }
}
