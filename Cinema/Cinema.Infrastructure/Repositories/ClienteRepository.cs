using Cinema.Domain.Entidades;
using Cinema.Domain.Interfaces;
using MongoDB.Driver;

namespace Cinema.Infrastructure.Repositories
{
    public class ClienteRepository(IMongoDatabase database) : IClienteRepository
    {
        private readonly IMongoCollection<Cliente> _clientes = database.GetCollection<Cliente>("Clientes");

        public async Task<Cliente> GetClienteAsync(string id)
        {
            return await _clientes.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateClienteAsync(Cliente cliente)
        {
            await _clientes.ReplaceOneAsync(c => c.Id == cliente.Id, cliente);
        }
    }
}
