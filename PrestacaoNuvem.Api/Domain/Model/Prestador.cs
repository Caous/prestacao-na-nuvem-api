namespace PrestacaoNuvem.Api.Domain.Model;

public class Prestador : Base
{
    public Prestador()
    {

    }
    public ETipoCadastro TipoCadastro { get; set; }
    public string? Nome { get; set; }
    public string? CPF { get; set; }
    public string? CpfRepresentante { get; set; }
    public string? CNPJ { get; set; }
    public string? RazaoSocial { get; set; }
    public string? NomeFantasia { get; set; }
    public string? NomeRepresentante { get; set; }
    public string? Telefone { get; set; }
    public string? EmailEmpresa { get; set; }
    public string? Endereco { get; set; }
    public string? EmailRepresentante { get; set; }
    public string? Logo {get; set; }
    public int SituacaoCadastral { get; set; }
    public DateTime? DataAbertura { get; set; }
    public DateTime? DataSituacaoCadastral { get; set; }
    public ICollection<PrestacaoServico>? OrdemServicos { get; set; }
    public ICollection<OrdemVenda>? OrdemVendas { get; set; }
    public ICollection<Servico>? Servicos { get; set; }
    public ICollection<CategoriaServico>? CategoriaServicos { get; set; }
    public ICollection<Cliente>? Clientes { get; set; }
    public ICollection<FuncionarioPrestador>? Funcionarios { get; set; }
    public ICollection<Produto>? Produtos { get; set; }
    public ICollection<Veiculo>? Veiculos { get; set; }
    public ICollection<Filial>? Filiais { get; set; }
}
