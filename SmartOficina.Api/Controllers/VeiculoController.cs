using Microsoft.AspNetCore.Mvc;
using SmartOficina.Api.Domain;
using SmartOficina.Api.Infrastructure.Repositories;

namespace SmartOficina.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoRepository _repository;
        public VeiculoController(IVeiculoRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(Veiculo veiculo)
        {
            return Ok(await _repository.Add(veiculo));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repository.GetAll());
        }
    }
}
