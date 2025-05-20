namespace PrestacaoNuvem.Api.Dto;

public class ContratoDto
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public DateTime DataCadastro { get; set; }
    public int Status { get; set; }
    public int TipoContrato { get; set; }
    public double Valor { get; set; }
    public ClienteDto Cliente { get; set; }
}
