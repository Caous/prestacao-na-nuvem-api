namespace SmartOficina.Api.Domain.Model;

public class Prestador : Base
{
    public required string Nome { get; set; }
    public string CPF { get; set; }
    public string CNPJ { get; set; }
    public string Razao_Social { get; set; }
    public string Nome_Fantasia { get; set; }
    public string Representante { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Endereco { get; set; }


    //<-- NOVO MODELO MODEL PRESTADOR --> 

    //public int IdPrestador { get; set; }
    //public required Guid PrestadorId { get; set; }
    //public required string TipoCadastro { get; set; }
    //public required string Nome { get; set; }
    //public string required CPF { get; set; }
    //public string required CNPJ { get; set; }
    //public string required Razao_Social { get; set; }
    //public string required Nome_Fantasia { get; set; }
    //public string requiredRepresentante { get; set; }
    //public string required Telefone { get; set; }
    //public string required Email { get; set; }
    //public string required Endereco { get; set; }
    //public CPF CpfRepresentante { get; set; }
    //public Email EmailRepresentante { get; set; }
    //public int required SituacaoCadastral { get; set; }
    //public DateTime required DataAbertura { get; set; }
    //public DateTime required DataSituacaoCadastral { get; set; }

    public ICollection<PrestacaoServico>? Servicos { get; set; }
}
