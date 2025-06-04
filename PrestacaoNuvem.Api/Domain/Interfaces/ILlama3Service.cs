namespace PrestacaoNuvem.Api.Domain.Interfaces;

public interface ILlama3Service
{
    Task<string> GerarAsync(AiGenerateRequestDto input);
}
