using Microsoft.AspNetCore.Mvc;
using PrestacaoNuvem.Api.Domain.Services;
using PrestacaoNuvem.Api.Dto;

namespace PrestacaoNuvem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendaEventoController : ControllerBase
    {
        private readonly IAgendaEventoService _service;
        public AgendaEventoController(IAgendaEventoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] AgendaEventoCreateDto dto)
        {
            var result = await _service.CriarEventoAsync(dto);
            return CreatedAtAction(nameof(BuscarPorId), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            var result = await _service.BuscarPorIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("periodo")]
        public async Task<IActionResult> BuscarPorPeriodo([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
        {
            var result = await _service.BuscarPorPeriodoAsync(inicio, fim);
            return Ok(result);
        }

        [HttpGet("funcionario/{funcionarioId}")]
        public async Task<IActionResult> BuscarPorFuncionario(Guid funcionarioId, [FromQuery] DateTime? inicio = null, [FromQuery] DateTime? fim = null)
        {
            var result = await _service.BuscarPorFuncionarioAsync(funcionarioId, inicio, fim);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] AgendaEventoCreateDto dto)
        {
            await _service.AtualizarEventoAsync(id, dto);
            return NoContent();
        }

        [HttpPut("{id}/cancelar")]
        public async Task<IActionResult> Cancelar(Guid id)
        {
            await _service.CancelarEventoAsync(id);
            return NoContent();
        }
    }
}
