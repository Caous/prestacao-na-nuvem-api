using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestacaoNuvem.Api.Domain.Interfaces;
using PrestacaoNuvem.Api.Domain.Model;
using PrestacaoNuvem.Api.Dto;
using PrestacaoNuvem.Api.Enumerations;

namespace PrestacaoNuvem.Api.Domain.Services
{
    public class AgendaEventoService : IAgendaEventoService
    {
        private readonly IAgendaEventoRepository _repository;
        public AgendaEventoService(IAgendaEventoRepository repository)
        {
            _repository = repository;
        }

        public async Task<AgendaEventoResponseDto> CriarEventoAsync(AgendaEventoCreateDto dto)
        {
            if (dto.Funcionarios == null || !dto.Funcionarios.Any())
                throw new ArgumentException("Evento deve ter pelo menos um funcionário vinculado.");

            var evento = new AgendaEvento
            {
                Id = Guid.NewGuid(),
                Descricao = dto.Descricao,
                DataHoraInicio = dto.DataHoraInicio,
                DataHoraFim = dto.DataHoraFim,
                Status = EStatusAgendaEvento.Criado,
                Localizacao = dto.Localizacao,
                Funcionarios = dto.Funcionarios.Select(f => new AgendaEventoFuncionario
                {
                    FuncionarioId = f.FuncionarioId,
                    CorIndicativa = f.CorIndicativa
                }).ToList()
            };
            await _repository.AddAsync(evento);
            return MapToResponseDto(evento);
        }

        public async Task<AgendaEventoResponseDto?> BuscarPorIdAsync(Guid id)
        {
            var evento = await _repository.GetByIdAsync(id);
            return evento == null ? null : MapToResponseDto(evento);
        }

        public async Task<IEnumerable<AgendaEventoResponseDto>> BuscarPorPeriodoAsync(DateTime inicio, DateTime fim)
        {
            var eventos = await _repository.GetByPeriodoAsync(inicio, fim);
            return eventos.Select(MapToResponseDto);
        }

        public async Task<IEnumerable<AgendaEventoResponseDto>> BuscarPorFuncionarioAsync(Guid funcionarioId, DateTime? inicio = null, DateTime? fim = null)
        {
            var eventos = await _repository.GetByFuncionarioAsync(funcionarioId, inicio, fim);
            return eventos.Select(MapToResponseDto);
        }

        public async Task CancelarEventoAsync(Guid id)
        {
            var evento = await _repository.GetByIdAsync(id);
            if (evento == null) throw new ArgumentException("Evento não encontrado.");
            evento.Status = EStatusAgendaEvento.Cancelado;
            await _repository.UpdateAsync(evento);
        }

        public async Task AtualizarEventoAsync(Guid id, AgendaEventoCreateDto dto)
        {
            var evento = await _repository.GetByIdAsync(id);
            if (evento == null) throw new ArgumentException("Evento não encontrado.");
            if (dto.Funcionarios == null || !dto.Funcionarios.Any())
                throw new ArgumentException("Evento deve ter pelo menos um funcionário vinculado.");
            evento.Descricao = dto.Descricao;
            evento.DataHoraInicio = dto.DataHoraInicio;
            evento.DataHoraFim = dto.DataHoraFim;
            evento.Localizacao = dto.Localizacao;
            evento.Funcionarios = dto.Funcionarios.Select(f => new AgendaEventoFuncionario
            {
                FuncionarioId = f.FuncionarioId,
                CorIndicativa = f.CorIndicativa
            }).ToList();
            await _repository.UpdateAsync(evento);
        }

        private AgendaEventoResponseDto MapToResponseDto(AgendaEvento evento)
        {
            return new AgendaEventoResponseDto
            {
                Id = evento.Id,
                Descricao = evento.Descricao,
                DataHoraInicio = evento.DataHoraInicio,
                DataHoraFim = evento.DataHoraFim,
                Localizacao = evento.Localizacao,
                Status = evento.Status.ToString(),
                Funcionarios = evento.Funcionarios.Select(f => new AgendaEventoFuncionarioDto
                {
                    FuncionarioId = f.FuncionarioId,
                    CorIndicativa = f.CorIndicativa
                }).ToList()
            };
        }
    }
}
