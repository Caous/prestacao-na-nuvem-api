using SmartOficina.Api.Domain;

namespace SmartOficina.Api.Infrastructure.Repositories
{
    public interface IPrestadorRepository
    {
        Task<Prestador> Add(Prestador prestador);
        Task<ICollection<Prestador>> GetAll();
    }
}
