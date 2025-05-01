using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using Azure.Storage.Blobs;
using DocumentFormat.OpenXml.Packaging;

namespace PrestacaoNuvem.Api.Domain.Services;

public class DocumentoService : IDocumentoService
{
    private readonly IPrestacaoServicoRepository _prestacaoServicoRepository;
    private readonly IMapper _mapper;
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _containerName = "modelos";
    private readonly string _blobName = "contrato_site.docx";
    private readonly IContratoService _contratoService;

    public DocumentoService(IPrestacaoServicoRepository prestacaoServicoRepository, IMapper mapper, BlobServiceClient blobServiceClient, IContratoService contratoService)
    {
        _prestacaoServicoRepository = prestacaoServicoRepository;
        _mapper = mapper;
        _blobServiceClient = blobServiceClient;
        _contratoService = contratoService;
    }

    async Task<byte[]> IDocumentoService.GerarContrato(ContratoRequestDto request)
    {
        string cabecalhoContrato = "";

        if (request.TipoCliente == 0) // Pessoa Jurídica
        {
            cabecalhoContrato = "NomeFantasia, pessoa jurídica de direito privado, inscrita no CNPJ n° NrCnpj, empresa regida pelas Leis brasileiras, com sede na EnderecoCompleto, doravante denominado CONTRATANTE e neste ato representada na forma de seus atos constitutivos, por seu representante legal NomeRepresentanteLegal, brasileiro, portador do CPF n° NrCpf residente e domiciliado na EnderecoRepre.";
        }
        else // Pessoa Física
        {
            cabecalhoContrato = "NomeContratante, brasileiro, portador do CPF n° NrCpf, RG n° NrRG residente e domiciliado na EnderecoCompleto, doravante denominado CONTRATANTE.";
        }

        // Faz replace das tags no texto
        cabecalhoContrato = cabecalhoContrato
            .Replace("NomeFantasia", WebUtility.HtmlEncode(request.NomeFantasia ?? ""))
            .Replace("NrCnpj", WebUtility.HtmlEncode(FormatCnpj(request.Cnpj)))
            .Replace("EnderecoCompleto", WebUtility.HtmlEncode(request.EnderecoEmpresa ?? ""))
            .Replace("NomeRepresentanteLegal", WebUtility.HtmlEncode(request.RepresentanteLegal ?? ""))
            .Replace("NrCpf", WebUtility.HtmlEncode(FormatCpf(request.CpfRepresentante)))
            .Replace("EnderecoRepre", WebUtility.HtmlEncode(request.EnderecoRepresentante ?? ""))
            .Replace("NomeContratante", WebUtility.HtmlEncode(request.NomeFantasia ?? ""))
            .Replace("NrRG", WebUtility.HtmlEncode(FormatRg(request.RgRepresentante) ?? ""));

        var dataPorExtenso = DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("pt-BR"));
        var dataFinal = $"{dataPorExtenso}, São Paulo";
        var linhasServico = string.Join("\n", request.Servicos.Select(s =>
                                $"• {s.Descricao} - R$ {s.Valor.ToString("N2", new CultureInfo("pt-BR"))}"
                            ));
        string formapagamento = $"O pagamento será efetuado por pix fornecido pela CONTRATADA. Chave Pix: 53.522.180/0001-38, Conta C6 Bank, 53.522.180 GUSTAVO SANTOS NASCIMENTO e cartão de crédito.";

        if (request.FormaPagamento == EFormaPagamento.AvistaPix)
            formapagamento = $"O pagamento será efetuado por pix fornecido pela CONTRATADA. Chave Pix: 53.522.180/0001-38, Conta C6 Bank, 53.522.180 GUSTAVO SANTOS NASCIMENTO";

        if (request.FormaPagamento == EFormaPagamento.CartaoCreditoAvista)
            formapagamento = $"O pagamento será efetuado via cartão de crédito avista pela Contratante. O link de pagamento será gerado de forma on-line para a CONTRATADA 53.522.180/0001-38, Conta C6 Bank, 53.522.180 GUSTAVO SANTOS NASCIMENTO";
        
        if (request.FormaPagamento == EFormaPagamento.CartaoCreditoAvista)
            formapagamento = $"O pagamento será efetuado via cartão de débito avista pela Contratante. O link de pagamento será gerado de forma on-line para a CONTRATADA 53.522.180/0001-38, Conta C6 Bank, 53.522.180 GUSTAVO SANTOS NASCIMENTO";

        if (request.FormaPagamento == EFormaPagamento.AvistaBoleto)
            formapagamento = $"O pagamento será efetuado via boleto bancário avista pela Contratante. O boleto será gerado pela CONTRATADA para a conta do CNPJ 53.522.180/0001-38, 53.522.180 GUSTAVO SANTOS NASCIMENTO";

        if (request.FormaPagamento == EFormaPagamento.CartaoCreditoParcelado)
            formapagamento = $"O pagamento será efetuado via cartão de crédito parcelado pela Contratante.  O link de pagamento será gerado de forma on-line para a CONTRATADA 53.522.180/0001-38, Conta C6 Bank, 53.522.180 GUSTAVO SANTOS NASCIMENTO";

        if (request.FormaPagamento == EFormaPagamento.CartaoCreditoParcelado)
            formapagamento = $"O pagamento será efetuado via cartão de crédito parcelado pela Contratante.  O link de pagamento será gerado de forma on-line para a CONTRATADA 53.522.180/0001-38, Conta C6 Bank, 53.522.180 GUSTAVO SANTOS NASCIMENTO";

        if (request.FormaPagamento == EFormaPagamento.ParceladoBoleto)
            formapagamento = $"O pagamento será efetuado via boleto bancário parcelado pela Contratante. O boleto será gerado pela CONTRATADA para a conta do CNPJ 53.522.180/0001-38, 53.522.180 GUSTAVO SANTOS NASCIMENTO";


        var dados = new Dictionary<string, string>
        {
                { "InfoContratante", WebUtility.HtmlEncode(cabecalhoContrato ?? "") },
                { "NomeFantasia", WebUtility.HtmlEncode(request.NomeFantasia ?? "") },
                { "NrCnpj", WebUtility.HtmlEncode(FormatCnpj(request.Cnpj)) },
                { "EnderecoCompleto", WebUtility.HtmlEncode(request.EnderecoEmpresa ?? "") },
                { "NomeRepresentanteLegal", WebUtility.HtmlEncode(request.RepresentanteLegal ?? "") },
                { "NrCpf", WebUtility.HtmlEncode(FormatCpf(request.CpfRepresentante)) },
                { "EnderecoRepre", WebUtility.HtmlEncode(request.EnderecoRepresentante ?? "") },
                { "DataContrato", dataFinal },
                { "TituloServico", WebUtility.HtmlEncode(request.TituloServico ?? "") },
                { "ListaServicos", WebUtility.HtmlEncode(linhasServico) },
                { "FormaPagamento",  WebUtility.HtmlEncode(formapagamento)},
                { "CondicaoPagamento",  WebUtility.HtmlEncode(request.CondicaoPagamento)}

        };

        // Fetch the file from Azure Blob Storage
        var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        var blobClient = containerClient.GetBlobClient(_blobName);

        if (!await blobClient.ExistsAsync())
            throw new FileNotFoundException("Arquivo de contrato modelo não encontrado no Azure Blob Storage", _blobName);

        using var memoryStream = new MemoryStream();
        await blobClient.DownloadToAsync(memoryStream);
        memoryStream.Position = 0;

        using (var wordDoc = WordprocessingDocument.Open(memoryStream, true))
        {
            var docText = string.Empty;

            using (var reader = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
            {
                docText = reader.ReadToEnd();
            }

            foreach (var item in dados)
            {
                var pattern = $"{item.Key}";
                docText = Regex.Replace(docText, pattern, item.Value, RegexOptions.IgnoreCase);
            }

            using (var writer = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
            {
                writer.Write(docText);
            }
        }

        var contrato = await _contratoService.CreateContrato(new(){
            ClienteId = request.ClienteId,
        });

        await UploadContratoForCustomer(contrato.Id, memoryStream.ToArray());

        return memoryStream.ToArray();
    }

    private async Task UploadContratoForCustomer(Guid contratoId, byte[] fileContent)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        var blobClient = containerClient.GetBlobClient($"{contratoId}.docx");

        using var stream = new MemoryStream(fileContent);
        await blobClient.UploadAsync(stream, overwrite: true);
    }

    public static string FormatCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return "";
        cpf = Regex.Replace(cpf, @"[^\d]", ""); // remove tudo que não for número
        return cpf.Length == 11
            ? Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00")
            : cpf;
    }

    public static string FormatCnpj(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj)) return "";
        cnpj = Regex.Replace(cnpj, @"[^\d]", ""); // remove tudo que não for número
        return cnpj.Length == 14
            ? Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00")
            : cnpj;
    }

    public static string FormatRg(string rg)
    {
        if (string.IsNullOrWhiteSpace(rg)) return "";
        rg = Regex.Replace(rg, @"[^\d]", ""); // remove tudo que não for número
        return rg.Length == 9
            ? Convert.ToUInt64(rg).ToString(@"00\.000\.000\-0")
            : rg;
    }
}
