using System.Net.Http.Headers;

namespace PrestacaoNuvem.Api.Domain.Services;

public class GptContractGenerator : IGptContractGenerator
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public GptContractGenerator(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _apiKey = config["OpenAI:ApiKey"];
    }

    public async Task<string> GerarContratoAsync(ContratoRequestDto request, string contratoModelo)
    {
        var prompt = MontarPromptComBaseNoContrato(request, contratoModelo);

        var body = new
        {
            model = "gpt-4",
            messages = new[]
            {
                new {
                    role = "system",
                    content = "Você é um advogado especialista em contratos de prestação de serviços. Responda sempre com formalidade e clareza."
                },
                new {
                    role = "user",
                    content = prompt
                }
            }
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

    private string MontarPromptComBaseNoContrato(ContratoRequestDto req, string contratoModelo)
    {
        var promptBase = """
                            Aja como um especialista jurídico com experiência avançada em direito contratual, especialmente na elaboração de contratos de prestação de serviços personalizados nas áreas de:

                            • Desenvolvimento de sistemas;
                            • Criação de sites;
                            • Social media;
                            • Consultoria;
                            • Hospedagem;
                            • Infraestrutura de TI.

                            Seu papel é analisar um modelo de contrato fornecido e adaptá-lo automaticamente conforme os dados do cliente e do serviço informado, garantindo:

                            1. Clareza nas obrigações e escopo do serviço;
                            2. Inclusão de cláusulas de proteção jurídica para a contratada;
                            3. Definição explícita de forma de pagamento, prazos, responsabilidade e suporte;
                            4. Ajuste de linguagem para Pessoa Jurídica ou Física;
                            5. Correção gramatical, formalidade e coerência legal;
                            6. Adição automática de cláusulas essenciais como:
                               - Propriedade intelectual
                               - Rescisão
                               - Sigilo e confidencialidade
                               - Suporte e manutenção (se aplicável)
                               - Responsabilidades das partes
                               - Penalidades por descumprimento
                            7. Adaptação do conteúdo com base na forma de pagamento.

                            O contrato deve ser gerado no formato de texto corrido e estruturado com clareza, pronto para substituir variáveis de um modelo `.docx`.
                                                        
                            Além disso, atue como um consultor jurídico especialista em contratos de prestação de serviço digital. Avalie se o conteúdo atual do contrato cobre adequadamente:

                            - Limitações de responsabilidade
                            - Prazos e escopo
                            - Propriedade intelectual do código/fontes entregues
                            - LGPD (Lei Geral de Proteção de Dados)
                            - Acordos de nível de serviço (SLA)
                            - Penalidades em caso de inadimplemento ou cancelamento
                            - Cláusulas sobre interrupção de serviço por força maior ou fatores externos

                            Se perceber ausência ou fragilidade em algum dos pontos acima, adicione cláusulas que atendam às boas práticas do direito brasileiro atual, com citação das leis se possível.

                            O objetivo é proteger a contratada juridicamente, evitar litígios e deixar obrigações e responsabilidades claras, sem ambiguidade.
                            
                            Seguem os dados do cliente:

                            """;

        var sb = new StringBuilder();
        sb.AppendLine(promptBase);
        sb.AppendLine();
        sb.AppendLine("Abaixo está o modelo atual de contrato que deve ser adaptado:");
        sb.AppendLine("----- INÍCIO DO MODELO -----");
        sb.AppendLine(contratoModelo);
        sb.AppendLine("----- FIM DO MODELO -----");
        sb.AppendLine();
        sb.AppendLine("Adapte este contrato com os dados acima, melhore a proteção jurídica e reescreva com linguagem mais forte, completa e juridicamente sólida.");


        sb.AppendLine($"Contrato entre a empresa: {req.NomeFantasia} - CNPJ {req.Cnpj}");
        sb.AppendLine($"Endereço da empresa: {req.EnderecoEmpresa}");
        sb.AppendLine($"Representada por: {req.RepresentanteLegal} - CPF {req.CpfRepresentante}, RG {req.RgRepresentante}, residente em {req.EnderecoRepresentante}");

        sb.AppendLine();
        sb.AppendLine("Serviços contratados:");
        foreach (var servico in req.Servicos)
        {
            sb.AppendLine($"- {servico.Descricao}, valor: R$ {servico.Valor:N2}");
        }

        sb.AppendLine();
        sb.AppendLine($"Forma de pagamento: {req.FormaPagamento}");
        sb.AppendLine($"Condição de pagamento: {req.CondicaoPagamento}");
        sb.AppendLine();
        sb.AppendLine($"Data de criação: {DateTime.Now:dd/MM/yyyy}");
        sb.AppendLine();
        sb.AppendLine("Gere um contrato com cláusulas claras de responsabilidade, escopo do serviço, valores, prazo e multa por descumprimento. O contrato deve ter linguagem formal, mas compreensível.");

        return sb.ToString();
    }

}
