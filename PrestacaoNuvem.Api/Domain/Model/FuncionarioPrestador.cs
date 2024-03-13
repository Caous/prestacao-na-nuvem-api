namespace PrestacaoNuvem.Api.Domain.Model;

public class FuncionarioPrestador : Base
{
    public FuncionarioPrestador()
    {

    }
    public Guid PrestadorId { get; set; }
    public Guid FilialId { get; set; }
    public required string Nome { get; set; }
    public required string Telefone { get; set; }
    public required string Email { get; set; }
    public required string RG { get; set; }
    //ToDo: Criar classe CPF
    public required string CPF { get; set; }
    //ToDo: Criar classe Endereço
    public string Endereco { get; set; }
    //ToDo: Criar classe Cargo
    public required string Cargo { get; set; }
    public Prestador Prestador { get; set; }
    public ICollection<PrestacaoServico>? OrdemServicos { get; set; }
    public Filial Filial { get; set; }
}
