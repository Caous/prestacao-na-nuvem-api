
using System.Net;

namespace PrestacaoNuvem.Api.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
[Produces("application/json")]
public class DocumentoController : MainController
{
    private readonly IDocumentoService _documentoService;

    public DocumentoController(IDocumentoService documentoService)
    {
        _documentoService = documentoService;
    }

    /// <summary>
    /// Gera proposta ou contrato a partir de um modelo Word
    /// </summary>
    /// <param name="id">Id da prestação de serviço</param>
    /// <returns>Arquivo gerado</returns>
    [HttpPost("gerar-proposta")]
    public async Task<IActionResult> GerarProposta([FromBody] ContratoRequestDto request)
    {
        var dados = new Dictionary<string, string>
            {
                { "NomeFantasia", WebUtility.HtmlEncode(request.NomeFantasia ?? "") },
                { "NrCnpj", WebUtility.HtmlEncode(request.Cnpj ?? "") },
                { "EnderecoCompleto", WebUtility.HtmlEncode(request.EnderecoEmpresa ?? "") },
                { "NomeRepresentanteLegal", WebUtility.HtmlEncode(request.RepresentanteLegal ?? "") },
                { "NrCpf", WebUtility.HtmlEncode(request.CpfRepresentante ?? "") },
                { "EnderecoCompletoRepresentante", WebUtility.HtmlEncode(request.EnderecoRepresentante ?? "") },
                { "DATACONTRATO", DateTime.Now.ToString("dd/MM/yyyy") },
                { "TituloServico", WebUtility.HtmlEncode(request.TituloServico ?? "") }
            };
        var result = await _documentoService.GerarContrato(dados);

        if (result == null || result.Length == 0)
            return NoContent();

        var fileName = $"Contrato_{request.NomeFantasia}.docx";
        return File(result, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
    }
}
