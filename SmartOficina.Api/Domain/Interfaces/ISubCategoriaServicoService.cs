namespace SmartOficina.Api.Domain.Interfaces;

public interface ISubCategoriaServicoService
{
    Task<ICollection<SubCategoriaServicoDto>> GetAllSubCategoria(SubCategoriaServicoDto item);
    Task<SubCategoriaServicoDto> FindByIdSubCategoria(Guid Id);
    Task<SubCategoriaServicoDto> CreateSubCategoria(SubCategoriaServicoDto item);
    Task<SubCategoriaServicoDto> UpdateSubCategoria(SubCategoriaServicoDto item);
    Task Delete(Guid Id);
    Task<SubCategoriaServicoDto> Desabled(Guid id);
}
