namespace SmartOficina.Api.Domain.Model;

public class Prestador : Base
{
    //public int Id_prestador { get; set; }
    //public required Guid PrestadorId { get; set; }
    public required string Nome { get; set; }
    public string CPF { get; set; }
    public string CNPJ { get; set; }
    public string Razao_Social { get; set; }
    public string? Nome_Fantasia { get; set; }
    public string? Representante { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public string Endereco { get; set; }


    public ICollection<PrestacaoServico>? Servicos { get; set; }
}
