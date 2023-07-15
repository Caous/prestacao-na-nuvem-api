using Microsoft.EntityFrameworkCore;
using SmartOficina.Api.Domain;
using SmartOficina.Api.Infrastructure.Context;

namespace SmartOficina.Api.Infrastructure.Repositories
{
    public class PrestacaoServicoRepository : IPrestacaoServicoRepository
    {
        private readonly OficinaContext _context;
        public PrestacaoServicoRepository(OficinaContext context)
        {
            _context = context;
        }
        public async Task<PrestacaoServico> Add(PrestacaoServico prestacaoServico)
        {
            prestacaoServico.Status = PrestacaoServicoStatus.Aberto;

            _context.PrestacaoServico.Add(prestacaoServico);
            await _context.SaveChangesAsync();

            return prestacaoServico;
        }

        public async Task ChangeStatus(Guid id, PrestacaoServicoStatus status)
        {
            var prestacao = await _context.PrestacaoServico.FindAsync(id);
            if (prestacao is not null)
            {
                prestacao.Status = status;
                await _context.SaveChangesAsync();
            }
            else throw new Exception("Prestacao não encontrada");
        }

        public async Task<ICollection<PrestacaoServico>> GetByPrestador(Guid prestadorId)
        {
            return await _context.PrestacaoServico.Where(f => f.PrestadorId == prestadorId).ToArrayAsync();
        }
    }
}
