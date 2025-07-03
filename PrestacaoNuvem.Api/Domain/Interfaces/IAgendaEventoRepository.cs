using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PrestacaoNuvem.Api.Domain.Model;

namespace PrestacaoNuvem.Api.Domain.Interfaces
{
    public interface IAgendaEventoRepository
    {
        Task<AgendaEvento> AddAsync(AgendaEvento evento);
        Task<AgendaEvento?> GetByIdAsync(Guid id);
        Task<IEnumerable<AgendaEvento>> GetByPeriodoAsync(DateTime inicio, DateTime fim);
        Task<IEnumerable<AgendaEvento>> GetByFuncionarioAsync(Guid funcionarioId, DateTime? inicio = null, DateTime? fim = null);
        Task UpdateAsync(AgendaEvento evento);
        Task DeleteAsync(Guid id);
    }
}
