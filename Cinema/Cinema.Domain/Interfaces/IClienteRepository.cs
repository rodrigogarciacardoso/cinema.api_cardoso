using Cinema.Domain.Entidades;

namespace Cinema.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente> GetClienteAsync(string id);
        Task UpdateClienteAsync(Cliente cliente);
    }
}
