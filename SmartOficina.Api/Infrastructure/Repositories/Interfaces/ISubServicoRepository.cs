namespace SmartOficina.Api.Infrastructure.Repositories.Interfaces;

public interface ISubServicoRepository : IGenericRepository<SubCategoriaServico>
{
    Task<ICollection<SubCategoriaServico>> GetAllWithIncludes();
}
