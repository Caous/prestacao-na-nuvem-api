namespace PrestacaoNuvem.Api.Domain.Interfaces;

public interface ISubCategoriaServicoService
{
    Task<ICollection<SubCategoriaServicoDto>> GetAllSubCategoria(SubCategoriaServicoDto item, bool admin);
    Task<SubCategoriaServicoDto> FindByIdSubCategoria(Guid Id);
    Task<SubCategoriaServicoDto> CreateSubCategoria(SubCategoriaServicoDto item);
    Task<SubCategoriaServicoDto> UpdateSubCategoria(SubCategoriaServicoDto item);
    Task Delete(Guid Id);
    Task<SubCategoriaServicoDto> Desabled(Guid id, Guid userDesabled);
}
