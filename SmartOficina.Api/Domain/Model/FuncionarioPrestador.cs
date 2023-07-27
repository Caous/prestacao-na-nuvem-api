namespace SmartOficina.Api.Domain.Model;

public class FuncionarioPrestador :  Base
{
    //ToDo: Colocar Id_Prestador
    //public int Id_prestador { get; set; }

    public required string Nome { get; set; }
    public required string? Telefone { get; set; }
    public required string? Email { get; set; } 
    public string RG { get; set; }
    //ToDo: Criar classe CPF
    public string CPF { get; set; }
    //ToDo: Criar classe Endereço
    public string Endereco { get; set; }
    //ToDo: Criar classe Cargo
    public required string Cargo { get; set; }
    public required ICollection<Prestador>? Empresa_Associada { get; set; }
}
