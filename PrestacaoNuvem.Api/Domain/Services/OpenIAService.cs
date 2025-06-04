using PrestacaoNuvem.Api.Enumerations;
using System.Net.Http.Headers;

namespace PrestacaoNuvem.Api.Domain.Services;

public class OpenIAService : IOpenIAService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private const string OpenAiEndpoint = "https://api.openai.com/v1/chat/completions";

    public OpenIAService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromSeconds(120);
        _apiKey = configuration["OpenAI:ApiKey"] ?? throw new ArgumentNullException("OpenAI:ApiKey not configured");
    }

    private string MontarPrompt(AiGenerateRequestDto input)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"Ação: {input.Tipo}");
        sb.AppendLine($"Configuração: {input.ConfiguracaoAcao}");
        sb.AppendLine("Conteúdo-base:");
        sb.AppendLine(input.Conteudo);

        if (input.Parametros is not null)
        {
            sb.AppendLine("Parâmetros adicionais:");
            foreach (var kv in input.Parametros)
                sb.AppendLine($"{kv.Key}: {kv.Value}");
        }

        return sb.ToString();
    }

    public async Task<string> GerarAsync(AiGenerateRequestDto input)
    {
        if (input.TipoAI != ETypeAI.OpenIA)
            throw new NotSupportedException("Este serviço suporta apenas chamadas para OpenAI");

        var prompt = MontarPrompt(input);

        var body = new
        {
            model = "gpt-4",
            messages = new[]
            {
                new { role = "system", content = "Você é um assistente útil de geração de conteúdo." },
                new { role = "user", content = prompt }
            }
        };

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

        var response = await _httpClient.PostAsync(OpenAiEndpoint,
            new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));

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
}
