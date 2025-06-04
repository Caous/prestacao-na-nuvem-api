namespace PrestacaoNuvem.Api.Domain.Interfaces;

public interface IGeminiService
{
    Task<string> GerarAsync(AiGenerateRequestDto input);
}
