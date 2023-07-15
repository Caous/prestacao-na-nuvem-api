using Microsoft.EntityFrameworkCore;
using SmartOficina.Api.Domain;
using SmartOficina.Api.Infrastructure.Context;

namespace SmartOficina.Api.Infrastructure.Repositories
{
    public class PrestadorRepository : IPrestadorRepository
    {
        private readonly OficinaContext _context;
        public PrestadorRepository(OficinaContext context)
        {
            _context = context;
        }
        public async Task<Prestador> Add(Prestador prestador)
        {
            var cli = await _context.Prestador.FirstOrDefaultAsync(f => f.Nome == prestador.Nome);

            if (cli is null)
            {
                _context.Prestador.Add(prestador);
                await _context.SaveChangesAsync();
                await _context.DisposeAsync();
                return prestador;
            }
            else throw new Exception("Prestador já existe!");
        }

        public async Task<ICollection<Prestador>> GetAll()
        {
            var result = await _context.Prestador.ToArrayAsync();
            await _context.DisposeAsync();

            return result;
        }
    }
}
