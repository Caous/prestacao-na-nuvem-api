namespace SmartOficina.Api.Controllers;

public abstract class MainController : ControllerBase
{

    public Guid PrestadorId
    {
        get
        {
            if (User.Claims.FirstOrDefault(c => c.Type == "PrestadorId") == null && !User.Claims.First(c => c.Type == "UserName").Value.Equals("OficinaNaNuvemAdm"))
                throw new Exception("Usuário logado de forma indevida");
            return new Guid(User.Claims.First(c => c.Type == "PrestadorId").Value);
        }
    }

    public Guid UserId
    {
        get
        {
            return new Guid(User.Claims.First(c => c.Type == "IdUserLogin").Value);

        }
    }

    public string UserName
    {
        get
        {
            return User.Claims.First(c => c.Type == "UserName").Value;
        }
    }

}
