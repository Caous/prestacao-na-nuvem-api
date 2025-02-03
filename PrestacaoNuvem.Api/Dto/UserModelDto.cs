namespace PrestacaoNuvem.Api.Dto;

public class UserModelDto : Base
{
    public string? UsrDescricaoDesativacao { get; set; }
    public Guid? FuncionarioId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }
}
