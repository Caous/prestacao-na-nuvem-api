namespace SmartOficina.Api.Dto;

public class UserOficinaDto : Base
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }
    public Guid IdUsuInclusao { get; set; }

}
