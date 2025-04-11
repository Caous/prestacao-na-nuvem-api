
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
    [HttpGet("gerar-proposta")]
    public async Task<IActionResult> GerarProposta([FromQuery] ContratoRequestDto request)
    {
        var dados = new Dictionary<string, string>
        {
            { "NomeFantasia", request.NomeFantasia },
            { "NrCnpj", request.Cnpj},
            { "EnderecoCompleto", request.EnderecoEmpresa},
            { "NomeRepresentanteLegal", request.RepresentanteLegal},
            { "NrCpf", request.CpfRepresentante },
            { "EnderecoCompletoRepresentante", request.EnderecoRepresentante},
            { "DATACONTRATO", DateTime.Now.ToString("dd/MM/yyyy") },
            { "TituloServico", request.TituloServico }
        };

        var result = await _documentoService.GerarContrato(dados);

        if (result == null || result.Length == 0)
            return NoContent();

        var fileName = $"Contrato_{request.NomeFantasia}.docx";
        return File(result, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
    }
}
