using System;
using System.Collections.Generic;
using PrestacaoNuvem.Api.Enumerations;

namespace PrestacaoNuvem.Api.Domain.Model
{
    public class AgendaEvento
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public EStatusAgendaEvento Status { get; set; }
        public string? Localizacao { get; set; }
        public ICollection<AgendaEventoFuncionario> Funcionarios { get; set; } = new List<AgendaEventoFuncionario>();
    }

    public class AgendaEventoFuncionario
    {
        public Guid AgendaEventoId { get; set; }
        public AgendaEvento AgendaEvento { get; set; } = null!;
        public Guid FuncionarioId { get; set; }
        public string CorIndicativa { get; set; } = string.Empty;
        // Se necessário, adicione a navegação para Funcionario futuramente
    }
}
