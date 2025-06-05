namespace PrestacaoNuvem.Api.Controllers;

/// <summary>
/// Controller dominio Whatsapp
/// </summary>
[Route("api/[controller]")]
[ApiController, Authorize]
[Produces("application/json")]
[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
public class WhatsappController : MainController
{
    private readonly IWhatsappService _serviceWhatsapp;

    public WhatsappController(IWhatsappService serviceWhatsapp)
    {
        _serviceWhatsapp = serviceWhatsapp;
    }

    [HttpPost("AddMessagens")]
    public async Task<IActionResult> AddMessages(string request)
    {
        ICollection<Message> messages = await _serviceWhatsapp.ValitadorMessageAsync(request);

        if (messages != null && messages.Any())
        {
            await _serviceWhatsapp.RegisterMessages(messages);
        }

        return Ok("Register Sucess");
    }

    [HttpGet("CountMessagesPending")]
    public async Task<IActionResult> CountMessagesPending()
    {

        int? result = await _serviceWhatsapp.CountMessagesPending(null);

        if (result == null)
            return NoContent();

        return Ok(result);

    }

    [HttpGet("CountNewTicketsSupport")]
    public async Task<IActionResult> CountNewTickets()
    {

        int? result = await _serviceWhatsapp.CountNewTicketsSupport(null);

        if (result == null)
            return NoContent();

        return Ok(result);

    }

    [HttpGet("CountRecurrenceCustomerMonth")]
    public async Task<IActionResult> CountRecurrenceCustomerMonth()
    {

        int? result = await _serviceWhatsapp.RecurrenceCustomer(null);

        if (result == null)
            return NoContent();

        return Ok(result);

    }

    [HttpGet("CountNewCustomer")]
    public async Task<IActionResult> CountNewCustomerMonth()
    {

        int? result = await _serviceWhatsapp.CountNewCustomerMonth(null);

        if (result == null)
            return NoContent();

        return Ok(result);

    }

    [HttpGet("GetLastMessagens")]
    public async Task<IActionResult> GetLastMessagens()
    {

        var result = await _serviceWhatsapp.GetLastMessagens(null);

        if (result == null)
            return NoContent();

        return Ok(result);

    }

    [HttpGet("GetAllMessagens")]
    public async Task<IActionResult> GetAllMessage()
    {

        BrokerLastMessagesResult result = await _serviceWhatsapp.GetMessages(null);

        if (result == null)
            return NoContent();

        return Ok(result);

    }

    [HttpGet("GetAllMessagensMongo")]
    public async Task<IActionResult> GetAllMessageMongo()
    {

        var result = await _serviceWhatsapp.GetAllAsync();

        if (result == null)
            return NoContent();

        return Ok(result);

    }
}
