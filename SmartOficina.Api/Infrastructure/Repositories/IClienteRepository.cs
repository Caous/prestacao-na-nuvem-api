using SmartOficina.Api.Domain;

namespace SmartOficina.Api.Infrastructure.Repositories
{
    public interface IClienteRepository
    {
        Task<Cliente> Add(Cliente cliente);
    }
}
