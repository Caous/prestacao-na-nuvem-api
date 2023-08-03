namespace SmartOficina.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SegurancaController : ControllerBase
{
    // GET: api/<SegurancaController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<SegurancaController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<SegurancaController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<SegurancaController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<SegurancaController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
