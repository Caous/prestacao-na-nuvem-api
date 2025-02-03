namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Interfaces;

public interface IAcessoManager
{
    Task<bool> CriarPrestador(PrestadorCadastroDto user);
    Task<bool> CriarFuncionario(UserModelDto user);
    Task<Token> ValidarCredenciais(UserModelDto user);
    Task<UserModel?> GetUserPorEmail(string email);
    Task<UserModel?> GetUserPorId(Guid id);
}
