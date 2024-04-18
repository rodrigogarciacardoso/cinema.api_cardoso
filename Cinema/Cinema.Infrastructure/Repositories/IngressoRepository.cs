using Cinema.Domain.Entidades;
using Cinema.Domain.Interfaces;
using Cinema.Infrastructure.Data.MongoDb.Documents;
using MongoDB.Driver;

namespace Cinema.Infrastructure.Repositories
{
    public class IngressoRepository(IMongoDatabase database, ISessaoRepository sessaoRepository, IClienteRepository clienteRepository) : IIngressoRepository
    {
        private readonly IMongoCollection<IngressoDocument> _ingressos = database.GetCollection<IngressoDocument>("Ingressos");
        private readonly ISessaoRepository _sessaoRepository = sessaoRepository;
        private readonly IClienteRepository _clienteRepository = clienteRepository;

        readonly Func<IngressoDocument, Sessao, Cliente, Ingresso> mapToDomain = (ingressoDocument, sessao, cliente) =>
            new Ingresso(ingressoDocument.Id!, sessao, ingressoDocument.Preco, ingressoDocument.Assento, cliente);

        readonly Func<Ingresso, IngressoDocument> mapToDocument = (ingresso) => new IngressoDocument
        {
            Id = ingresso.Id,
            SessaoId = ingresso.Sessao.Id,
            Preco = ingresso.Preco,
            Assento = ingresso.Assento,
            ClienteId = ingresso.Cliente.Id
        };

        public async Task<Ingresso> GetIngressoAsync(string id)
        {
            var ingressoDocument = await _ingressos.Find(i => i.Id == id).FirstOrDefaultAsync();
            var sessao = await _sessaoRepository.GetSessaoAsync(ingressoDocument.SessaoId);
            var cliente = await _clienteRepository.GetClienteAsync(ingressoDocument.ClienteId);

            return mapToDomain(ingressoDocument, sessao, cliente);
        }

        public async Task<IEnumerable<Ingresso>> GetIngressosPorClienteAsync(string id)
        {
            var ingressoDocument = await _ingressos.Find(i => i.ClienteId == id).ToListAsync();

            var sessaoIds = ingressoDocument.Select(s => s.SessaoId).Distinct().ToList();
            var sessoes = await _sessaoRepository.GetSessoesAsync(sessaoIds);

            var cliente = await _clienteRepository.GetClienteAsync(id);

            var sessaoDictionary = sessoes.ToDictionary(f => f.Id);
            
            return ingressoDocument.Select(i => mapToDomain(i, sessaoDictionary[i.SessaoId], cliente));
        }

        public async Task UpdateIngressoAsync(Ingresso ingresso)
        {
            var ingressoDocument = mapToDocument(ingresso);

            await _ingressos.ReplaceOneAsync(i => i.Id == ingressoDocument.Id, ingressoDocument);
        }
    }
}