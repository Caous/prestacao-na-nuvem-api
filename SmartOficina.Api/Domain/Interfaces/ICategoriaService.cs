namespace SmartOficina.Api.Domain.Interfaces;

public interface ICategoriaService
{
    Task<ICollection<CategoriaServicoDto>> GetAllCategoria(CategoriaServicoDto item);
    Task<CategoriaServicoDto> FindByIdCategoria(Guid Id);
    Task<CategoriaServicoDto> CreateCategoria(CategoriaServicoDto item);
    Task<CategoriaServicoDto> UpdateCategoria(CategoriaServicoDto item);
    Task Delete(Guid Id);
    Task<CategoriaServicoDto> Desabled(Guid id);

}
