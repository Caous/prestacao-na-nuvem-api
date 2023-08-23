

namespace SmartOficina.Api.Infrastructure.Repositories.Services
{
    public class FuncionarioPrestadorServiceRepository : GenericRepository<FuncionarioPrestador>, IFuncionarioPrestadorRepository
    {
        private readonly OficinaContext _context;
        public FuncionarioPrestadorServiceRepository(OficinaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<FuncionarioPrestador>> GetListaFuncionarioPrestadorAsync(Guid id)
        {
            var result = _context.FuncionarioPrestador
                 .Where(f => f.PrestadorId == id)
                 .Include(i => i.Prestador)
                 .ToList();
            await _context.DisposeAsync();

            return result;
        }
    }
}
