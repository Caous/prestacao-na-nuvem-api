namespace PrestacaoNuvem.Api.Domain.Interfaces;

public interface IGptContractGenerator
{
    Task<string> GerarContratoAsync(ContratoRequestDto request, string contratoModelo);
}
