namespace SmartOficina.Api.Domain.Model;

public class FuncionarioPrestador :  Base
{
    //ToDo: Colocar Id_Prestador
    //public int Id_prestador { get; set; }
    //public required Guid PrestadorId { get; set; }
    public required string Nome { get; set; }
    //ToDo: Colocar required
    public string? Telefone { get; set; }
    //ToDo: Colocar required
    public string? Email { get; set; }
    //ToDo: Colocar required
    public string RG { get; set; }
    //ToDo: Criar classe CPF
    public string CPF { get; set; }
    //ToDo: Criar classe Endereço
    public string Endereco { get; set; }
    //ToDo: Criar classe Cargo
    public string Cargo { get; set; }
    //ToDo: Colocar required
    public ICollection<Prestador>? Empresa_Associada { get; set; }
}
