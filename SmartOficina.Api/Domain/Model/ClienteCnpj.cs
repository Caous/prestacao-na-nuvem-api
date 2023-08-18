namespace SmartOficina.Api.Domain.Model;

public class ClienteCnpj
{
    public ClienteCnpj()
    {

    }
    public Cpf CpfRepresentante { get; set; }
    public Cnpj CNPJ { get; set; }
    public string RazaoSocial { get; set; }
    public string NomeFantasia { get; set; }
    public string NomeRepresentante { get; set; }
    public string Telefone { get; set; }
    public string EmailEmpresa { get; set; }
    public string Endereco { get; set; }
    public string EmailRepresentante { get; set; }
    public int SituacaoCadastral { get; set; }
    public DateTime DataAbertura { get; set; }
    public DateTime DataSituacaoCadastral { get; set; }

}
