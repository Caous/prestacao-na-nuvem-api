namespace PrestacaoNuvem.Api.Domain.Model;

public class Contrato
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public Cliente Cliente { get; set; } 
}
