namespace PrestacaoNuvem.Seguranca.Dto;
public abstract class Base
{
    public Guid? Id { get; set; }
    public DateTime DataCadastro { get; set; }
    public Guid UsrCadastro { get; set; }
    public string UsrDescricaoCadastro { get; set; }
    public DateTime? DataDesativacao { get; set; }
    public Guid? UsrDesativacao { get; set; }
    public string? UsrDescricaoDesativacao { get; set; }
    public Guid? PrestadorId { get; set; }
}
