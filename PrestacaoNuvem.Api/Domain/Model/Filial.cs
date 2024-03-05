namespace PrestacaoNuvem.Api.Domain.Model;

public class Filial : Base
{
    public string Nome { get; set; }
    public string Observacao { get; set; }
    public string Logradouro { get; set; }
    public int CEP { get; set; }
    public int Numero { get; set; }
    public bool Matriz { get; set; }
    public Guid IdGerenteFilial { get; set; }
}
