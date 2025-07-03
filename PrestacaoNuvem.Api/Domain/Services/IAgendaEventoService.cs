using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PrestacaoNuvem.Api.Dto;

namespace PrestacaoNuvem.Api.Domain.Services
{
    public interface IAgendaEventoService
    {
        Task<AgendaEventoResponseDto> CriarEventoAsync(AgendaEventoCreateDto dto);
        Task<AgendaEventoResponseDto?> BuscarPorIdAsync(Guid id);
        Task<IEnumerable<AgendaEventoResponseDto>> BuscarPorPeriodoAsync(DateTime inicio, DateTime fim);
        Task<IEnumerable<AgendaEventoResponseDto>> BuscarPorFuncionarioAsync(Guid funcionarioId, DateTime? inicio = null, DateTime? fim = null);
        Task CancelarEventoAsync(Guid id);
        Task AtualizarEventoAsync(Guid id, AgendaEventoCreateDto dto);
    }
}
