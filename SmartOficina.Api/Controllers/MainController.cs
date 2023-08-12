namespace SmartOficina.Api.Controllers;

public abstract class MainController : ControllerBase
{

    public Guid PrestadorId
    {
        get
        {
            return new Guid(User.Claims.FirstOrDefault(c => c.Type == "PrestadorId").Value);
        }
    }

    public Guid UserId
    {
        get
        {
            return new Guid(User.Claims.FirstOrDefault(c => c.Type == "IdUserLogin").Value);

        }
    }

    public string UserName
    {
        get
        {
            return User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
        }
    }

}
