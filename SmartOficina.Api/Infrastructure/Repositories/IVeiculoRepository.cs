using SmartOficina.Api.Domain;

namespace SmartOficina.Api.Infrastructure.Repositories
{
    public interface IVeiculoRepository
    {
        Task<Veiculo> Add(Veiculo veiculo);
        Task<ICollection<Veiculo>> GetAll();
    }
}
