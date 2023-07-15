using Microsoft.AspNetCore.Mvc;
using SmartOficina.Api.Domain;
using SmartOficina.Api.Infrastructure.Repositories;

namespace SmartOficina.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _repository;
        public ClienteController(IClienteRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(Cliente cliente)
        {
            return Ok(await _repository.Add(cliente));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repository.GetAll());
        }
    }
}
