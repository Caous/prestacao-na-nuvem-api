using Microsoft.AspNetCore.Mvc;
using SmartOficina.Api.Domain;
using SmartOficina.Api.Infrastructure.Repositories;

namespace SmartOficina.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestadorController : ControllerBase
    {
        private readonly IPrestadorRepository _repository;
        public PrestadorController(IPrestadorRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(Prestador prestador)
        {
            return Ok(await _repository.Add(prestador));
        }
    }
}
