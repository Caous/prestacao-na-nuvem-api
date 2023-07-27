namespace SmartOficina.Api.Dto;
//ToDo: Colocar os campos com os mesmos required e opcional em relação ao model
//ToDo: Colocar validação no fluentValidation na pasta Validators e depois disso colocar a injeção de dependência dentro de infrastructure vai achar aonde referencia
public class ProdutoDto
{
    public Guid? Id { get; set; }
    public string Nome { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public DateTime Data_validade { get; set; }
    public string Garantia { get; set; }
    public float Valor_Compra { get; set; }
    public float Valor_Venda { get; set; }
}
