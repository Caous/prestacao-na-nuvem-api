using System.Net;
using Azure.Storage.Blobs;

namespace PrestacaoNuvem.Api.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
[Produces("application/json")]
public class DocumentoController : MainController
{
    private readonly IDocumentoService _documentoService;
    private readonly BlobServiceClient _blobServiceClient;

    public DocumentoController(IDocumentoService documentoService, BlobServiceClient blobServiceClient)
    {
        _documentoService = documentoService;
        _blobServiceClient = blobServiceClient;
    }

    /// <summary>
    /// Gera proposta ou contrato a partir de um modelo Word
    /// </summary>
    /// <param name="id">Id da prestação de serviço</param>
    /// <returns>Arquivo gerado</returns>
    [HttpPost("gerar-proposta")]
    public async Task<IActionResult> GerarProposta([FromBody] ContratoRequestDto request)
    {
        var result = await _documentoService.GerarContrato(request);

        if (result == null || result.Length == 0)
            return NoContent();

        var nameRefactor = request.NomeFantasia?.Replace(" ", "_");
        var fileName = $"Contrato_{nameRefactor}.docx";

        return File(result, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
    }
}
