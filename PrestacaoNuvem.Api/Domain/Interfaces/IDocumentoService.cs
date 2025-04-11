namespace PrestacaoNuvem.Api.Domain.Interfaces;

public interface IDocumentoService
{
    /// <summary>
    /// Gera o documento Word (proposta ou contrato) com base no ID da ordem de serviço
    /// </summary>
    /// <param name="prestacaoServicoId">ID da prestação de serviço</param>
    /// <param name="prestadorId">ID do prestador logado</param>
    /// <returns>Arquivo em byte array</returns>
    Task<byte[]> GerarContrato(ContratoRequestDto request);
}
