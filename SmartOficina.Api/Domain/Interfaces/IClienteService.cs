namespace SmartOficina.Api.Domain.Interfaces;

public interface IClienteService
{
    Task<ICollection<ClienteDto>> GetAllCliente(ClienteDto item);
    Task<ClienteDto> FindByIdCliente(Guid Id);
    Task<ClienteDto> CreateCliente(ClienteDto item);
    Task<ClienteDto> UpdateCliente(ClienteDto item);
    Task Delete(Guid Id);
    Task<ClienteDto> Desabled(Guid id, Guid userDesabled);
}
