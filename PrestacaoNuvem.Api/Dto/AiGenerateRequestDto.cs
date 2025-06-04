using PrestacaoNuvem.Api.Enumerations;

namespace PrestacaoNuvem.Api.Dto;

public class AiGenerateRequestDto
{
    /// <summary>
    /// Tipo da ação desejada: "gerar_texto", "resumir", "analisar_dialogo", "classificar", etc.
    /// </summary>
    public string Tipo { get; set; } = string.Empty;

    /// <summary>
    /// Configuração da ação:
    /// </summary>
    public string ConfiguracaoAcao { get; set; } = string.Empty;

    /// <summary>
    /// Conteúdo base para a IA executar a ação.
    /// </summary>
    public string Conteudo { get; set; } = string.Empty;

    public ETypeAI TipoAI { get; set; }

    /// <summary>
    /// Parâmetros extras se necessário (ex: tom, categoria, idioma).
    /// </summary>
    public Dictionary<string, string>? Parametros { get; set; }
}
