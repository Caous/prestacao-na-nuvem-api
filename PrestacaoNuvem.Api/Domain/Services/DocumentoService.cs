using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace PrestacaoNuvem.Api.Domain.Services;

public class DocumentoService : IDocumentoService
{
    private readonly IPrestacaoServicoRepository _prestacaoServicoRepository;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;
    private readonly string _caminhoContrato;

    public DocumentoService(IPrestacaoServicoRepository prestacaoServicoRepository, IMapper mapper, IWebHostEnvironment env)
    {
        _prestacaoServicoRepository = prestacaoServicoRepository;
        _mapper = mapper;
        _env = env;

        // caminho raiz do projeto (ideal em dev e produção)
        _caminhoContrato = Path.Combine(_env.ContentRootPath, "Files", "Contrato_Site.docx");
    }

    Task<byte[]> IDocumentoService.GerarContrato(Dictionary<string, string> dadosParaReplace)
    {        
        // Garante que o arquivo de template existe
        if (!File.Exists(_caminhoContrato))
            throw new FileNotFoundException("Arquivo de contrato modelo não encontrado", _caminhoContrato);

        // Copia o arquivo para um stream de memória para edição
        byte[] byteArray = File.ReadAllBytes(_caminhoContrato);
        using var memoryStream = new MemoryStream();
        memoryStream.Write(byteArray, 0, byteArray.Length);

        using (var wordDoc = WordprocessingDocument.Open(memoryStream, true))
        {
            var docText = string.Empty;

            using (var reader = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
            {
                docText = reader.ReadToEnd();
            }

            foreach (var item in dadosParaReplace)
            {
                var pattern = $"{item.Key}";
                docText = Regex.Replace(docText, pattern, item.Value, RegexOptions.IgnoreCase);
            }

            using (var writer = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
            {
                writer.Write(docText);
            }
        }
        byte[] result = memoryStream.ToArray();
        return Task.FromResult(result);
    }
}
