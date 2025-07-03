using System;
using System.Collections.Generic;

namespace PrestacaoNuvem.Api.Dto
{
    public class AgendaEventoCreateDto
    {
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public string? Localizacao { get; set; }
        public List<AgendaEventoFuncionarioDto> Funcionarios { get; set; } = new();
    }

    public class AgendaEventoFuncionarioDto
    {
        public Guid FuncionarioId { get; set; }
        public string CorIndicativa { get; set; } = string.Empty;
    }

    public class AgendaEventoResponseDto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public string? Localizacao { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<AgendaEventoFuncionarioDto> Funcionarios { get; set; } = new();
    }
}
