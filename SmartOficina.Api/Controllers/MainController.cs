using System.Security.Claims;

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

}
