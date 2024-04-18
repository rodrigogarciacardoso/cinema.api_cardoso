using Cinema.Domain.Entidades;
using Cinema.Domain.Interfaces;
using Cinema.Infrastructure.Data.MongoDb.Documents;
using MongoDB.Driver;

namespace Cinema.Infrastructure.Repositories
{
    public class ClienteRepository(IMongoDatabase database, IIngressoRepository ingressoRepository) : IClienteRepository
    {
        private readonly IMongoCollection<ClienteDocument> _clientes = database.GetCollection<ClienteDocument>("Clientes");
        private readonly IIngressoRepository _ingressoRepository = ingressoRepository;

        readonly Func<ClienteDocument, List<Ingresso>, Cliente> mapToDomain = (clienteDocument, ingressos) => 
            new Cliente(clienteDocument.Id!, clienteDocument.Nome, clienteDocument.Email, clienteDocument.Telefone, ingressos);

        readonly Func<Cliente, ClienteDocument> mapToDocument = (cliente) => new ClienteDocument
        {
            Id = cliente.Id,
            Nome = cliente.Nome,
            Email = cliente.Email,
            Telefone = cliente.Telefone
        };

        public async Task<Cliente> GetClienteAsync(string id)
        {
            var clienteDocument = await _clientes.Find(c => c.Id == id).FirstOrDefaultAsync();            
            var ingressos = (await _ingressoRepository.GetIngressosPorClienteAsync(id)).ToList();

            return mapToDomain(clienteDocument, ingressos);
        }

        public async Task UpdateClienteAsync(Cliente cliente)
        {
            var clienteDocument = mapToDocument(cliente);

            await _clientes.ReplaceOneAsync(c => c.Id == clienteDocument.Id, clienteDocument);
        }
    }
}
