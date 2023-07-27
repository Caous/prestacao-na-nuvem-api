namespace SmartOficina.Api.Domain.Model;

public abstract class Base
{
    public Guid Id { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataDesativacao { get; set; }
    public Guid UsrCadastro { get; set; }
    public Guid? UsrDesativacao { get; set; }
}
