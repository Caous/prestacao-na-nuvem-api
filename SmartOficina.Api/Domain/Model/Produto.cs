namespace SmartOficina.Api.Domain.Model
{
    public class Produto : Base
    {
        public string Nome { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public DateTime Data_validade { get; set; }
        public string Garantia { get; set; }
        public float Valor_Compra { get; set; }
        public float Valor_Venda { get; set; }
    }
}
