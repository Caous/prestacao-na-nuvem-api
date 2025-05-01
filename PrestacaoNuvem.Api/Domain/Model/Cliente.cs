namespace PrestacaoNuvem.Api.Domain.Model;

public class Cliente : Base
{
    public required string Nome { get; set; }
    public required string Telefone { get; set; }
    public required string Email { get; set; }
    public string? Rg { get; set; }
    //ToDo: Criar classe de CPF
    public required string CPF { get; set; }
    //ToDo: Criar classe de Endereço
    public string? Endereco { get; set; }
    public string? Categoria { get; set; }
    public string? HorarioFuncionamento { get; set; }
    public string? Nota { get; set; }
    public string? WebSite { get; set; }
    public string? RedesSociais { get; set; }
    public int Status { get; set; }
    public string? Observacao { get; set; }
    public string? BoxEmail { get; set; }
    public string? NomeRepresentante { get; set; }
    public string? CNPJ { get; set; }
    public int? TipoCliente { get; set; }
    public ICollection<HistoricoCliente>? Historico { get; set; }
    public required Guid PrestadorId { get; set; }
    public Prestador Prestador { get; set; }
    public ICollection<PrestacaoServico>? Servicos { get; set; }
    public ICollection<OrdemVenda>? OrdemVendas { get; set; }
    public ICollection<Contrato>? Contratos { get; set; }

}