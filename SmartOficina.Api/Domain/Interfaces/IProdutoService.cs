namespace SmartOficina.Api.Domain.Interfaces;

public interface IProdutoService
{
    Task<ICollection<ProdutoDto>> GetAllProduto(ProdutoDto item);
    Task<ProdutoDto> FindByIdProduto(Guid Id);
    Task<ProdutoDto> CreateProduto(ProdutoDto item);
    Task<ICollection<ProdutoDto>?> MapperProdutoLot(IFormFile file);
    Task<string> CreateProdutoLot(ICollection<ProdutoDto> itens);
    Task<ProdutoDto> UpdateProduto(ProdutoDto item);
    Task Delete(Guid Id);
    Task<ProdutoDto> Desabled(Guid id, Guid userDesabled);
}
