namespace SmartOficina.Api.Infrastructure.Repositories.Interfaces;

public interface IAcessoManager
{
    Task<bool> CriarFornecedor(UserAutenticationDto user);
    Task<bool> CriarFuncionario(UserAutenticationDto user);
    Task<Token> ValidarCredenciais(UserAutenticationDto user);
    Task<UserAutentication> GetUserPorEmail(string email);
}
