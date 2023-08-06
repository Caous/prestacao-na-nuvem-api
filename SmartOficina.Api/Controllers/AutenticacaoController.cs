namespace SmartOficina.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutenticacaoController : ControllerBase
{
    // GET: api/<AutenticacaoController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<AutenticacaoController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<AutenticacaoController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<AutenticacaoController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<AutenticacaoController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
