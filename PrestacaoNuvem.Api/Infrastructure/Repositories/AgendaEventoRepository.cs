using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PrestacaoNuvem.Api.Domain.Model;
using PrestacaoNuvem.Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using PrestacaoNuvem.Api.Infrastructure.Context;

namespace PrestacaoNuvem.Api.Infrastructure.Repositories
{
    public class AgendaEventoRepository : IAgendaEventoRepository
    {
        private readonly OficinaContext _context;
        public AgendaEventoRepository(OficinaContext context)
        {
            _context = context;
        }

        public async Task<AgendaEvento> AddAsync(AgendaEvento evento)
        {
            _context.AgendaEventos.Add(evento);
            await _context.SaveChangesAsync();
            return evento;
        }

        public async Task<AgendaEvento?> GetByIdAsync(Guid id)
        {
            return await _context.AgendaEventos
                .Include(e => e.Funcionarios)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<AgendaEvento>> GetByPeriodoAsync(DateTime inicio, DateTime fim)
        {
            return await _context.AgendaEventos
                .Include(e => e.Funcionarios)
                .Where(e => e.DataHoraInicio >= inicio && e.DataHoraFim <= fim)
                .ToListAsync();
        }

        public async Task<IEnumerable<AgendaEvento>> GetByFuncionarioAsync(Guid funcionarioId, DateTime? inicio = null, DateTime? fim = null)
        {
            var query = _context.AgendaEventos
                .Include(e => e.Funcionarios)
                .Where(e => e.Funcionarios.Any(f => f.FuncionarioId == funcionarioId));
            if (inicio.HasValue)
                query = query.Where(e => e.DataHoraInicio >= inicio.Value);
            if (fim.HasValue)
                query = query.Where(e => e.DataHoraFim <= fim.Value);
            return await query.ToListAsync();
        }

        public async Task UpdateAsync(AgendaEvento evento)
        {
            _context.AgendaEventos.Update(evento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.AgendaEventos.FindAsync(id);
            if (entity != null)
            {
                _context.AgendaEventos.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
