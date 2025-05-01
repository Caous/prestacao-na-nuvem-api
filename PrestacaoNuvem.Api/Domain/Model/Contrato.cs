namespace PrestacaoNuvem.Api.Domain.Model;

public class Contrato
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public Cliente Cliente { get; set; } 
    public DateTime DataCadastro { get; set; }
    public int Status { get; set; }
    public int TipoContrato { get; set; }
    public double Valor { get; set; }
}
