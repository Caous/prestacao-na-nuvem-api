namespace SmartOficina.Api.Dto;
//ToDo: Colocar os campos com os mesmos required e opcional em relação ao model
//ToDo: Colocar validação no fluentValidation na pasta Validators e depois disso colocar a injeção de dependência dentro de infrastructure vai achar aonde referencia
public class VeiculoDto
{
    public required string Placa { get; set; }
    public required string Marca { get; set; }
    public required string Modelo { get; set; }
    public EVeiculoTipo Tipo { get; set; }
    public Guid? Id { get; set; }
}
