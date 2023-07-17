namespace SmartOficina.Api.Domain.Model;

public abstract class Base
{
    public Guid Id { get; set; }
    public DateTime DataCadastro { get; set; }
}
