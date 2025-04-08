namespace PrestacaoNuvem.Api.Controllers;

public abstract class MainController : ControllerBase
{
    public bool IsAdminLogged
    {
        get
        {
            return User.Claims.FirstOrDefault(c => c.Type == "UserName")?.Value == "admOficinaNaNuvem";
        }
    }

    public Guid PrestadorId
    {
        get
        {
            if (IsAdminLogged)
                return Guid.Empty;

            var prestadorClaim = User.Claims.FirstOrDefault(c => c.Type == "PrestadorId");

            if (prestadorClaim == null || string.IsNullOrWhiteSpace(prestadorClaim.Value))
                throw new Exception("Usuário logado de forma indevida - PrestadorId inválido");

            return new Guid(prestadorClaim.Value);
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

    public Guid FuncionarioId
    {
        get
        {
            return new Guid(User.Claims.First(c => c.Type == "FuncionarioId").Value);
        }
    }
}