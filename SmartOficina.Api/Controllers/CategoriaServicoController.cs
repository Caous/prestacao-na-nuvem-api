namespace SmartOficina.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriaServicoController : ControllerBase
{
    // GET: api/<CategoriaServicoController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<CategoriaServicoController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<CategoriaServicoController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<CategoriaServicoController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<CategoriaServicoController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
