namespace PrestacaoNuvem.Api.Dto;

public class FilialDto : Base
{
    public string Nome { get; set; } = string.Empty;
    public string? Observacao { get; set; }
    public string Logradouro { get; set; } = string.Empty;
    public string CEP { get; set; } = string.Empty;
    public int Numero { get; set; }
    public bool Matriz { get; set; }
    public Guid? IdGerenteFilial { get; set; }
}
