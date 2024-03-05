using PrestacaoNuvem.Api.Domain.Interfacesk;

namespace PrestacaoNuvem.Api.Controllers;

/// <summary>
/// Controller Email Serviço
/// </summary>
[Route("api/[controller]")]
[ApiController,Authorize]
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
        emailConfig.FromEmail = "caous.g@gmail.com";
        emailConfig.ToEmail = new string[] { "caous.g@gmail.com", "gustavo.nascimento@innovasfera.com.br" };
        emailConfig.Menssage = "Fazendo um teste pelo meu sistema :)";

        return Ok(await _emailManager.SendEmailSmtpAsync(emailConfig));
    }
}

