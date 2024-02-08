namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Interfaces
{
    public interface IFuncionarioPrestadorRepository : IGenericRepository<FuncionarioPrestador>
    {
        Task<ICollection<FuncionarioPrestador>> GetListaFuncionarioPrestadorAsync(Guid id, FuncionarioPrestador filter);
    }
}
