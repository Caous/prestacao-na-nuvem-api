using SmartOficina.Api.Domain.Model;

namespace SmartOficina.Api.Infrastructure.Repositories.Services;

public class CategoriaServicoRepository : GenericRepository<CategoriaServico>, ICategoriaServicoRepository
{
    public CategoriaServicoRepository(OficinaContext context) : base(context)
    {
    }
}
