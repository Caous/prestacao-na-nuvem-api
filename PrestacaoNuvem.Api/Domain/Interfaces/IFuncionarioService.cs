namespace PrestacaoNuvem.Api.Domain.Interfaces;

public interface IFuncionarioService
{
    Task<ICollection<FuncionarioPrestadorDto>> GetAllFuncionario(FuncionarioPrestadorDto item);
    Task<FuncionarioPrestadorDto> FindByIdFuncionario(Guid Id);
    Task<FuncionarioPrestadorDto> CreateFuncionario(FuncionarioPrestadorDto item);
    Task<FuncionarioPrestadorDto> UpdateFuncionario(FuncionarioPrestadorDto item);
    Task Delete(Guid Id);
    Task<FuncionarioPrestadorDto> Desabled(Guid id, Guid userDesabled);
}
