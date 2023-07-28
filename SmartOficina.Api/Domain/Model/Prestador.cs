namespace SmartOficina.Api.Domain.Model;

public class Prestador : Base
{
    
    public int IdPrestador { get; set; }
    public required Guid PrestadorId { get; set; }
    public required int TipoCadastro { get; set; }
    public required string Nome { get; set; }
    public required string CPF { get; set; }
    public string?  CpfRepresentante { get; set; }
    public required string CNPJ { get; set; }
    public required string  RazaoSocial { get; set; }
    public required string NomeFantasia { get; set; }
    public required string  NomeRepresentante { get; set; }
    public required string Telefone { get; set; }
    public required string EmailEmpresa { get; set; }
    public required string Endereco { get; set; }
    public string? EmailRepresentante { get; set; }
    public required int SituacaoCadastral { get; set; }
    public  DateTime DataAbertura { get; set; }
    public  DateTime DataSituacaoCadastral { get; set; }
    public ICollection<PrestacaoServico>? Servicos { get; set; }
}
