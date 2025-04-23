using Azure.Storage.Blobs;

namespace PrestacaoNuvem.Api.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
public class GustaTechController : MainController
{
    private readonly BlobServiceClient _blobServiceClient;

    public GustaTechController(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    [HttpGet("Upload")]
    public async Task<IActionResult> GetAll()
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient("contratos");
        var blobClient = containerClient.GetBlobClient("gusta-tech-hacker.txt");

        var downloadResponse = await blobClient.DownloadContentAsync();
        var downloadedContent = downloadResponse.Value.Content.ToString();
        Console.WriteLine($"Downloaded content: {downloadedContent}");

        await blobClient.UploadAsync(new MemoryStream(Encoding.UTF8.GetBytes("Teste")), true);

        return Ok("Arquivo enviado com sucesso!");
    }
}