using PrestacaoNuvem.Api.Domain.Interfacesk;
using PrestacaoNuvem.Api.Domain.Model;

namespace PrestacaoNuvem.Api.Controllers;

/// <summary>
/// Controller Email Serviço
/// </summary>
[Route("api/[controller]")]
[ApiController, Authorize]
[Produces("application/json")]
[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
public class EmailController : MainController
{
    private readonly IEmailManager _emailManager;
    private readonly IConfiguration _configuration;

    public EmailController(IEmailManager emailManager, IConfiguration configuration)
    {
        _emailManager = emailManager;
        _configuration = configuration;
    }

    /// <summary>
    /// Controller de teste para envio de e-mail
    /// </summary>
    /// <param name="cliente"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> SendAsyncEmail()
    {

        Email emailConfig = new(
            new EmailConfigHost(
                _configuration.GetValue<string>("EmailConfiguration:Host"),
                _configuration.GetValue<int>("EmailConfiguration:Port"),
                _configuration.GetValue<string>("EmailConfiguration:UserName"),
               _configuration.GetValue<string>("EmailConfiguration:Password")));

        emailConfig.Subject = "E-mail teste - Prestação na Nuvem";
        emailConfig.FromEmail = _configuration.GetValue<string>("EmailConfiguration:UserName");
        emailConfig.ToEmail = new string[] { "caous.g@gmail.com" };
        emailConfig.Menssage = "Fazendo um teste pelo meu sistema :)";

        return Ok(await _emailManager.SendEmailSmtpAsync(emailConfig));
    }

    /// <summary>
    /// Envio e-mail Proposta
    /// </summary>
    /// <param name="cliente"></param>
    /// <returns></returns>
    [HttpPost("PostEmailProposta")]
    public async Task<IActionResult> PostEmailPropostaAsync(EmailRequestPropostaDto request)
    {
        var result = await _emailManager.PostPropostaEmailAsync(request);

        if (result != null)
            return Ok();
        else
            return BadRequest("Usuário já existente");
    }

    /// <summary>
    /// Envia e-mail de proposta com PDF em anexo
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("PostEmailPropostaComAnexo")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> PostEmailPropostaComAnexoAsync([FromForm] EmailRequestContratoFileDto request)
    {
        if (request.PdfAnexo == null || request.PdfAnexo.Length == 0)
            return BadRequest("Nenhum arquivo enviado");

        using var memoryStream = new MemoryStream();
        await request.PdfAnexo.CopyToAsync(memoryStream);
        var pdfBytes = memoryStream.ToArray();

        var result = await _emailManager.PostPropostaEmailComAnexoAsync(request, pdfBytes);

        return result ? Ok() : BadRequest("Erro ao enviar e-mail");
    }
}

