namespace PrestacaoNuvem.Api.Domain.Interfaces;

public interface IOpenIAService
{
    Task<string> GerarAsync(AiGenerateRequestDto input);
}
