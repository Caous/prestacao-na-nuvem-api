namespace SmartOficina.Api.Dto;

public abstract class Base
{
    public Guid? Id { get; set; }
    public DateTime? DataCadastro { get; set; }
    public DateTime? DataDesativacao { get; set; }
    public Guid? UsrCadastro { get; set; }
    public string? UsrCadastroDesc { get; set; }
    public Guid? PrestadorId { get; set; }
    public Guid? UsrDesativacao { get; set; }
}
