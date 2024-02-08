namespace PrestacaoNuvem.Api.Dto; 

public class FuncionarioPrestadorDto : Base
{
    public FuncionarioPrestadorDto()
    {
           
    }
    public required string Nome { get; set; }// 200
    public required string Telefone { get; set; }// 11
    public required string Email { get; set; }// 250
    public required string RG { get; set; }// 9 
    public required string CPF { get; set; }// 9 
    public required string Endereco { get; set; }// 250
    public required string Cargo { get; set; }// 100
}
