using PrestacaoNuvem.Api.Enumerations;

namespace PrestacaoNuvem.Api.Domain.Services;

public class AiGenerateService : IAiGenerateService
{
    private readonly IOpenIAService _openIAService;
    private readonly ILlama3Service _llama3Service;
    private readonly IGeminiService _geminiService;

    public AiGenerateService(
        IOpenIAService openIAService,
        ILlama3Service llama3Service,
        IGeminiService geminiService)
    {
        _openIAService = openIAService;
        _llama3Service = llama3Service;
        _geminiService = geminiService;
    }

    public async Task<string> GenerateAsync(AiGenerateRequestDto input)
    {
        return input.TipoAI switch
        {
            ETypeAI.OpenIA => await _openIAService.GerarAsync(input),
            ETypeAI.Llama3 => await _llama3Service.GerarAsync(input),
            ETypeAI.Gemini => await _geminiService.GerarAsync(input),
            _ => throw new ArgumentOutOfRangeException(nameof(input.TipoAI), "Tipo de IA não suportado."),
        };
    }
}

