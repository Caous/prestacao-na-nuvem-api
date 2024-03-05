namespace PrestacaoNuvem.Api.Dto;

public class FilialDto : Base
{
    public string Nome { get; set; }
    public string Observacao { get; set; }
    public string Logradouro { get; set; }
    public int CEP { get; set; }
    public int Numero { get; set; }
    public bool Matriz { get; set; }
    public Guid IdGerenteFilial { get; set; }
}
