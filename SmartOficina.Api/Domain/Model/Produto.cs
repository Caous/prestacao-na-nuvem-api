namespace SmartOficina.Api.Domain.Model;
public class Produto : Base
{
    //ToDo: Colocar required
    public string Nome { get; set; }
    //ToDo: Colocar required
    public string Marca { get; set; }
    //ToDo: Colocar required
    public string Modelo { get; set; }
    //ToDo: Colocar Opcional
    public DateTime Data_validade { get; set; }
    //ToDo: Colocar Opcional
    public string Garantia { get; set; }
    //ToDo: Colocar required acima de 0.01 centavos
    public float Valor_Compra { get; set; }
    //ToDo: Colocar required acima de 0.01 centavos
    public float Valor_Venda { get; set; }
}
