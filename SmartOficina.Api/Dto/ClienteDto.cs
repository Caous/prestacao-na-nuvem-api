namespace SmartOficina.Api.Dto;

//ToDo: Colocar os campos com os mesmos required e opcional em relação ao model
//ToDo: Colocar validação no fluentValidation na pasta Validators e depois disso colocar a injeção de dependência dentro de infrastructure vai achar aonde referencia
public class ClienteDto
{
    public ClienteDto()
    {
        
    }
    public required string Nome { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public string RG { get; set; }
    public string CPF { get; set; }
    public string Endereco { get; set; }
    public Guid? Id { get; set; }
}
