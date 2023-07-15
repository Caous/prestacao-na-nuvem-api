using Microsoft.EntityFrameworkCore;
using SmartOficina.Api.Domain;
using SmartOficina.Api.Infrastructure.Context;

namespace SmartOficina.Api.Infrastructure.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly OficinaContext _context;
        public VeiculoRepository(OficinaContext context)
        {
            _context = context;
        }
        public async Task<Veiculo> Add(Veiculo veiculo)
        {
            var cli = await _context.Veiculo.FirstOrDefaultAsync(f => f.Placa == veiculo.Placa);

            if (cli is null)
            {
                _context.Veiculo.Add(veiculo);
                await _context.SaveChangesAsync();
                await _context.DisposeAsync();
                return veiculo;
            }
            else throw new Exception("Veiculo já existe!");
        }

        public async Task<ICollection<Veiculo>> GetAll()
        {
            var result = await _context.Veiculo.ToArrayAsync();
            await _context.DisposeAsync();

            return result;
        }
    }
}
