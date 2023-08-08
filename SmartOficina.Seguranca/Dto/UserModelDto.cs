namespace SmartOficina.Seguranca.Dto;

public class UserModelDto : Base
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }
    public Guid IdUsuInclusao { get; set; }

}
