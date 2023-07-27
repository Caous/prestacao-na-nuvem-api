namespace SmartOficina.Api.Domain.Model;

public class Cliente : Base
{
    public required string Nome { get; set; }
    //ToDo: Colocar required
    public string Telefone { get; set; }
    //ToDo: Colocar required
    public string? Email { get; set; }
    //ToDo: Colocar required
    public string Rg { get; set; }
    //ToDo: Criar classe de CPF
    public string CPF { get; set; }
    //ToDo: Criar classe de Endereço
    public string Endereco { get; set; }
    public ICollection<PrestacaoServico>? Servicos { get; set; }

}