using Microsoft.EntityFrameworkCore;
using SmartOficina.Api.Domain;
using SmartOficina.Api.Infrastructure.Context;

namespace SmartOficina.Api.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly OficinaContext _context;
        public ClienteRepository(OficinaContext context)
        {
            _context = context;
        }
        public async Task<Cliente> Add(Cliente cliente)
        {
            var cli = await _context.Cliente.FirstOrDefaultAsync(f => f.Nome == cliente.Nome);

            if (cli is null)
            {
                _context.Cliente.Add(cliente);
                await _context.SaveChangesAsync();
                return cliente;
            }
            else throw new Exception("Cliente já existe!");
        }
    }
}
