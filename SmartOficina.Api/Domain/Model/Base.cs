namespace SmartOficina.Api.Domain.Model;

//ToDo: Colocar campo usuário que adicinou required
//ToDo: Colocar campo usuário que excluiu/Desativado opcional
//ToDo: Colocar campo data de Desativação opcional
public abstract class Base
{
    public Guid Id { get; set; }
    public DateTime DataCadastro { get; set; }
}
