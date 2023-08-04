namespace SmartOficina.Api.Infrastructure.Repositories.Services;

public class AcessoManager : IAcessoManager
{
    public AcessoManager()
    {
        
    }
    public Task<bool> CriarFornecedor(UserAutenticationDto user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CriarFuncionario(UserAutenticationDto user)
    {
        throw new NotImplementedException();
    }

    public Task<UserAutentication> GetUserPorEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<Token> ValidarCredenciais(UserAutenticationDto user)
    {
        throw new NotImplementedException();
    }
}
