namespace PrestacaoNuvem.Api.Controllers;

/// <summary>
/// Controller de Dashboard
/// </summary>
[Route("api/[controller]")]
[ApiController, Authorize]
[Produces("application/json")]
[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
public class AiGenerateController : MainController
{
    private readonly IAiGenerateService _aiGenerateService;

    public AiGenerateController(IAiGenerateService aiGenerateService)
    {
        _aiGenerateService = aiGenerateService;
    }

    /// <summary>
    /// Gera um conteúdo baseado em tipo e configuração.
    /// </summary>
    /// <param name="request">Parâmetros da requisição</param>
    /// <returns>Texto gerado pela IA</returns>
    [HttpPost]
    public async Task<IActionResult> GenerateAsync([FromBody] AiGenerateRequestDto request)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        try
        {
            var result = await _aiGenerateService.GenerateAsync(request);

            if (string.IsNullOrWhiteSpace(result))
                return NoContent();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao gerar conteúdo: {ex.Message}");
        }
    }
}
