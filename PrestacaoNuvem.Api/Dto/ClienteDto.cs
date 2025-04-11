namespace PrestacaoNuvem.Api.Dto;

public class ClienteDto : Base
{
    public ClienteDto()
    {

    }
    public required string Nome { get; set; } //Obri Character 125
    public required string Telefone { get; set; } //Obri Character 14 
    public required string Email { get; set; } //Obri Character 250
    public string? Rg { get; set; } //Op Character 9
    public required string CPF { get; set; } //Obri Character 11
    public string? Endereco { get; set; } //Obri Character 250
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
    public ICollection<HistoricoClienteDto>? Historico { get; set; }
}
