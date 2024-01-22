namespace SmartOficina.Seguranca.Infrastructure.Configurations.Repositories.Interfaces;

public interface IAcessoManager
{
    Task<bool> CriarPrestador(PrestadorCadastroDto user);
    Task<bool> CriarFuncionario(UserModelDto user);
    Task<Token> ValidarCredenciais(UserModelDto user);
    Task<UserModel?> GetUserPorEmail(string email);
}
