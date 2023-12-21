namespace SmartOficina.Api.Domain.Interfaces;

public interface IClienteService
{
    Task<ICollection<ClienteDto>> GetAllProduto(ClienteDto item);
    Task<ClienteDto> FindByIdProduto(Guid Id);
    Task<ClienteDto> CreateProduto(ClienteDto item);
    Task<ClienteDto> UpdateProduto(ClienteDto item);
    Task Delete(Guid Id);
    Task<ClienteDto> Desabled(Guid id);
}
