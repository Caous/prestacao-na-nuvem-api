namespace PrestacaoNuvem.Api.Domain.Interfaces;

public interface IAiGenerateService
{
    /// <summary>
    /// Gera um conteúdo baseado em tipo e parâmetros configuráveis.
    /// </summary>
    /// <param name="input">Entrada com tipo de ação e conteúdo-base.</param>
    /// <returns>Resposta gerada pela IA.</returns>
    Task<string> GenerateAsync(AiGenerateRequestDto input);
}
