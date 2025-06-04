using System.Net.Http.Headers;

namespace PrestacaoNuvem.Api.Domain.Services;

public class GptContractGenerator : IGptContractGenerator
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public GptContractGenerator(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromSeconds(300);
        _apiKey = config["OpenAI:ApiKey"];
    }

    public async Task<string> GerarContratoAsync(ContratoRequestDto request, string contratoModelo)
    {
        var mensagens = MontarMensagensEmEtapas(request, contratoModelo);

        var body = new
        {
            model = "gpt-4",
            messages = mensagens
        };

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

        var response = await _httpClient.PostAsync(
            "https://api.openai.com/v1/chat/completions",
            new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
        );

        if (!response.IsSuccessStatusCode)
        {
            var erro = await response.Content.ReadAsStringAsync();
            throw new Exception($"Erro do GPT: {erro}");
        }

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);

        return doc.RootElement
                  .GetProperty("choices")[0]
                  .GetProperty("message")
                  .GetProperty("content")
                  .GetString()!;
    }

    private List<object> MontarMensagensEmEtapas(ContratoRequestDto req, string contratoModelo)
    {
        var sb = new StringBuilder();

        sb.AppendLine("Você é um advogado especialista em direito contratual, com profundo conhecimento em prestação de serviços digitais e tecnologia. Analise o contrato abaixo, sem simplificá-lo, e apenas adapte os dados do cliente e serviços contratados conforme necessário.");
        sb.AppendLine();
        sb.AppendLine("Este contrato já contém cláusulas importantes e bem estruturadas que devem ser mantidas. Seu papel é:");
        sb.AppendLine("1. Adaptar com base nos dados fornecidos (cliente, serviços, valores, forma de pagamento);");
        sb.AppendLine("2. Reforçar a linguagem jurídica apenas onde necessário;");
        sb.AppendLine("3. Incluir cláusulas adicionais apenas se houver ausência de pontos críticos legais como:");
        sb.AppendLine("   - LGPD;");
        sb.AppendLine("   - Propriedade intelectual;");
        sb.AppendLine("   - SLA e suporte;");
        sb.AppendLine("   - Penalidades contratuais;");
        sb.AppendLine("   - Rescisão e confidencialidade.");
        sb.AppendLine();
        sb.AppendLine("Evite remover cláusulas existentes. Mantenha a estrutura geral. Melhore o que for necessário, sem perder o conteúdo original do modelo.");
        sb.AppendLine();
        sb.AppendLine("### Dados do cliente:");
        sb.AppendLine(MontarDadosCliente(req));
        sb.AppendLine();
        sb.AppendLine("### Modelo de contrato a ser adaptado:");
        sb.AppendLine("----- INÍCIO DO CONTRATO MODELO -----");
        sb.AppendLine(contratoModelo);
        sb.AppendLine("----- FIM DO CONTRATO MODELO -----");
        sb.AppendLine();
        sb.AppendLine("Adapte esse contrato com base nos dados fornecidos. O contrato final deve ter linguagem jurídica, clara, e formal. Não inclua observações ou instruções, apenas o contrato final completo.");

        var mensagens = new List<object>
        {
            new {
                role = "system",
                content = "Você é um advogado especialista em contratos de prestação de serviços. Responda sempre com formalidade e clareza."
            },
            new {
                role = "user",
                content = sb.ToString()
            }
        };

        return mensagens;
    }

    private string MontarDadosCliente(ContratoRequestDto req)
    {
        var sb = new StringBuilder();

        sb.AppendLine("Dados do Cliente:");
        sb.AppendLine($"- Empresa: {req.NomeFantasia} - CNPJ: {req.Cnpj}");
        sb.AppendLine($"- Endereço: {req.EnderecoEmpresa}");
        sb.AppendLine($"- Representante: {req.RepresentanteLegal} - CPF: {req.CpfRepresentante} - RG: {req.RgRepresentante}");
        sb.AppendLine($"- Endereço do representante: {req.EnderecoRepresentante}");
        sb.AppendLine();

        sb.AppendLine("Serviços contratados:");
        foreach (var servico in req.Servicos)
        {
            sb.AppendLine($"• {servico.Descricao} - R$ {servico.Valor:N2}");
        }

        sb.AppendLine();
        sb.AppendLine($"Forma de pagamento: {req.FormaPagamento}");
        sb.AppendLine($"Condição de pagamento: {req.CondicaoPagamento}");
        sb.AppendLine($"Data de criação: {DateTime.Now:dd/MM/yyyy}");

        return sb.ToString();
    }

}
