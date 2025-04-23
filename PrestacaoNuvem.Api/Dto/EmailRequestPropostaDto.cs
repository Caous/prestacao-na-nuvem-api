namespace PrestacaoNuvem.Api.Dto;

public class EmailRequestPropostaDto : EmailRequestDto
{
    public PrestacaoServicoDto OrdemServico { get; set; }
}
