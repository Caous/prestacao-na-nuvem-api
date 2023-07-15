using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartOficina.Api.Domain;
using SmartOficina.Api.Infrastructure.Repositories;

namespace SmartOficina.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestacaoServicoController : ControllerBase
    {
        private readonly IPrestacaoServicoRepository _repository;
        public PrestacaoServicoController(IPrestacaoServicoRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Add(PrestacaoServico prestacaoServico)
        {
            return Ok(await _repository.Add(prestacaoServico));
        }

        [HttpPut("status/{id}/{status}")]
        public async Task<IActionResult> ChangeStatus(Guid id, PrestacaoServicoStatus status)
        {
            await _repository.ChangeStatus(id, status);
            return Ok();
        }

        [HttpGet("{prestadorId}")]
        public async Task<IActionResult> GetByPrestador(Guid prestadorId)
        {
            return Ok(await _repository.GetByPrestador(prestadorId));
        }
    }
}
